module.exports = function (ngModule) {
    ngModule.directive("signalr", function (signalRHubProxy) {
        return {
            restrict: "E",
            template: require("./signalr.html"),
            controllerAs: "vm",
            controller: function () {
                var vm = this;
                vm.currentTime = '';
                var client = signalRHubProxy('myHub', {logging: true});

                client.on('sendData', function (data) {
                    vm.currentTime = data;
                });

                client.connection.disconnected(function () {
                    setTimeout(function () {
                        client.connection.start();
                    }, 5000);
                });
            }
        };
    });
};