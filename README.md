# Crypto-exchanges-quotes-harvester
<p>Console application for gathering market data from Binance and Poloniex crypto-exchanges.</p>
<p>Data as quotes(with properties: symbol, bid, ask, exchange, time created) is stored on SQL Server.</p>
<p>At the start of application settings menu appear where can shoose default settings or set new settings by choise.</p>
<p>Settings menu:
<ul>
  <li>Set flush-period. That is a time interval in wich data is stored in database.</li>
  <li>Set list of regular instruments to be stored.</li>
  <li>(optional)Set list of synthetic instruments from wich regular instruments are calculated in the process and stored.</li>
</ul>
</p>
<p>In the process log messages are created when server or application errors occur and stored in additional table</p>
