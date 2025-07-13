using AiTradingDemo.Models;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace AiTradingDemo.Services;

public class PricePredictionService
{
    // MLContext is the starting point for all ML.NET operations
    private readonly MLContext _mlContext = new();

    // Model input class represents features used for prediction
    public class ModelInput
    {
        [LoadColumn(0)]  // Load the first column as feature
        public float MovingAverage { get; set; }
    }

    // Model output class represents prediction result
    public class ModelOutput
    {
        [ColumnName("Score")]  // The predicted value is stored in 'Score' column
        public float PredictedClose { get; set; }
    }

    // This method trains the model on past data and predicts next price
    public void TrainAndPredict(List<Candle> candles)
    {
        // Select candles with a calculated moving average as training inputs
        var trainingData = candles
            .Where(c => c.MovingAverage > 0)
            .Select(c => new ModelInput
            {
                MovingAverage = (float)c.MovingAverage
            }).ToList();

        // Select corresponding close prices as labels
        var labels = candles
            .Where(c => c.MovingAverage > 0)
            .Select(c => (float)c.Close)
            .ToList();

        // If not enough data, skip training
        if (trainingData.Count < 10)
        {
            Console.WriteLine("🚫Insufficient data for training.");
            return;
        }

        // Combine inputs and labels into training data schema
        var data = _mlContext.Data.LoadFromEnumerable(trainingData.Zip(labels, (input, label) =>
        {
            return new ModelTrainRow { MovingAverage = input.MovingAverage, Close = label };
        }));

        // Define pipeline:
        // - Copy Close as Label for supervised learning
        // - Combine features into 'Features' vector
        // - Use SDCA regression trainer
        var pipeline = _mlContext.Transforms.Concatenate("Features", "MovingAverage")
             .Append(_mlContext.Regression.Trainers.Sdca(labelColumnName: "Close", featureColumnName: "Features"));

        // Train the model
        var model = pipeline.Fit(data);

        // Predict closing price based on the last moving average value
        var lastMA = (float)candles.Last().MovingAverage;
        var predictionEngine = _mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
        var prediction = predictionEngine.Predict(new ModelInput { MovingAverage = lastMA });

        Console.WriteLine($"📈 Predict the price based on MA({lastMA}): {prediction.PredictedClose:F2} USD");
    }

    // Internal class to hold training data rows
    private class ModelTrainRow
    {
        public float MovingAverage { get; set; }
        public float Close { get; set; }
    }
}