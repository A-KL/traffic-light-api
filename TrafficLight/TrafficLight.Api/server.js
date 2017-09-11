'use strict';
var http = require('http');
var uwp = require("uwp");

var port = process.env.PORT || 1337;

uwp.projectNamespace("Windows");

var gpioController = Windows.Devices.Gpio.GpioController.getDefault();
var pin = gpioController.openPin(5);
pin.setDriveMode(Windows.Devices.Gpio.GpioPinDriveMode.output)
pin.write(Windows.Devices.Gpio.GpioPinValue.high);

setInterval(function () {
    if (pin.read() == Windows.Devices.Gpio.GpioPinValue.high) {
        pin.write(Windows.Devices.Gpio.GpioPinValue.low);
    } else {
        pin.write(Windows.Devices.Gpio.GpioPinValue.high);
    }
}, 1000);

http.createServer(function (req, res) {
    res.writeHead(200, { 'Content-Type': 'text/plain' });
    res.end('Hello World\n');
}).listen(port);

uwp.close();