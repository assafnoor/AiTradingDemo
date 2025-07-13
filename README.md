
# AiTradingDemo

**AiTradingDemo** is a simple demo project built with C# and ML.NET to predict trading prices based on technical indicators such as Moving Average and Relative Strength Index (RSI).

---

## 🚀 Project Goal

Develop a predictive model that uses candlestick market data to forecast the next closing price based on well-known technical indicators like Moving Average, RSI, and optionally Volume.

---

## 🛠️ Technologies Used

* .NET 9
* C#
* ML.NET (for machine learning and predictions)
* SQLite (optional, for storing market data)
* LINQ and Collections for data processing

---

## 📦 Project Structure

* **Models/**
  Contains the `Candle` data model.

* **Services/**
  Contains the `PricePredictionService` responsible for training the ML model, calculating technical indicators, and saving/loading the model.

* **Program.cs**
  The application entry point that loads data, trains the model, and makes price predictions.

---

## ⚙️ How to Use

### 1. Prepare Data

Make sure you have candlestick data (`Candles`) with essential fields:
`Open, Close, High, Low, Volume` (if available).

### 2. Train and Save Model

```csharp
var service = new PricePredictionService();
service.TrainAndSaveModel(candles); // candles is a list of candlestick data
```

### 3. Load Model and Predict

```csharp
service.LoadModelAndPredict(candles);
```

---

## 🔍 Important Details

* **RSI Calculation**
  RSI is calculated within `PricePredictionService` using the `CalculateRSI` method.

* **Model Features**
  The model uses Moving Average, RSI, and Volume as input features.

* **Model Persistence**
  The trained model is saved to `model.zip` file for later reuse without retraining.

---

## 💡 Future Improvements

* Add more technical indicators (MACD, Bollinger Bands, etc.).
* Improve user interface.
* Add graphical visualization of predictions.
* Integrate with real trading APIs.

---

## 📄 License

This project is open-source under the MIT License.

---

## 📞 Contact

If you have any questions or suggestions, feel free to reach out via email or GitHub.
