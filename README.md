# TradingView Bot

## Overview

Welcome to the TradingView Bot repository! This project is a comprehensive trading bot developed in C# as a Windows Service, utilizing SQL Server for data storage. The bot leverages the TradingView API to fetch EMA and MACD crossover signals for all NSE stocks across multiple time frames. It includes features such as volume checks, analyst ratings, and the latest news for specific tickers. The bot can execute trades based on buy signals, manage stop-loss and trailing stop-loss, and send notifications to a Telegram channel.

Note: This project is for educational purposes only.

## Features

- **EMA and MACD Crossover Signals**: Detects EMA and MACD crossovers for all NSE stocks across multiple time frames.
- **Volume and Analyst Ratings**: Checks trading volume and analyst ratings for each stock.
- **Latest News**: Fetches the latest news for specific tickers.
- **Trade Execution**: Executes trades based on buy signals and manages stop-loss and trailing stop-loss.
- **Signal Tracking**: Saves all signals to a SQL Server database for tracking trades and buy signals.
- **Telegram Notifications**: Sends notifications to a Telegram channel for new buy signals, trade executions, and daily/monthly reports.
- **Daily and Monthly Reports**: Sends reports at 9 AM and 4 PM every day, detailing executed trades, profit/loss, running trades, and closed trades.

## Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/shashilpu/TVBOT.git
   ```

2. **Navigate to the project directory**:
   ```bash
   cd TVBOT
   ```

3. **Build the project** using your preferred IDE (e.g., Visual Studio).

4. **Configure the SQL Server database**:
   - Update the connection string in the `appsettings.json` file.
   - Run the provided SQL scripts to set up the necessary database tables.

5. **Configure the TradingView API**:
   - Update the API keys and other relevant settings in the `appsettings.json` file.

6. **Set up the Windows Service**:
   - Install the service using the `InstallUtil.exe` tool or through the Visual Studio Installer.

## Usage

1. **Start the Windows Service**:
   - Ensure the service is running to begin monitoring and executing trades.

2. **Monitor Telegram Notifications**:
   - Join the configured Telegram channel to receive real-time notifications for buy signals, trade executions, and reports.

## Contributing

We welcome contributions to enhance the functionality of this TradingView Bot. Please follow these steps to contribute:

1. **Fork the repository**.
2. **Create a new branch**:
   ```bash
   git checkout -b feature/YourFeatureName
   ```
3. **Make your changes** and commit them:
   ```bash
   git commit -m 'Add some feature'
   ```
4. **Push to the branch**:
   ```bash
   git push origin feature/YourFeatureName
   ```
5. **Open a pull request**.

## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Contact

For any questions or support, please open an issue or contact the repository owner.

---

Feel free to customize this README further to suit your project's needs! If you need any more help, just let me know.@shashilpu@gmail.com 😊