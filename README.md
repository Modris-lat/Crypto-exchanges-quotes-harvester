# Crypto-exchanges-quotes-harvester
<p>Console application for gathering market data from <strong>Binance</strong> and <strong>Poloniex</strong> crypto-exchanges.</p>
<p>Data as quotes(with properties: symbol, bid, ask, exchange, time created) is stored on SQL Server.</p>
<p>At the start of application settings menu appear where can shoose default settings or set new settings by choise.</p>
<p>Settings menu(default settings can be set, if the same values required for multiple app starts):
<ul>
  <li>Set flush-period. That is a time interval for data stored in database.</li>
  <li>Set list of regular instruments to be stored.</li>
  <li>(optional)Set list of synthetic instruments from which regular instruments are calculated in the process and stored. Synthetic input is case sensitive(instrument1 instrument2). Instrument2 will always be divided by instrument1. Correct input required.</li>
</ul>
</p>
<p>In the process log messages are created when server or application errors occur and stored in additional table</p>
<p><strong>Api keys needs to be changed for different users</strong></p>
<p>All unit tests must pass before starting application</p>
