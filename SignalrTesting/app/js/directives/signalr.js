module.exports = function (ngModule) {
    ngModule.directive("signalr", function (signalrHubProxy) {
        return {
            restrict: "E",
            template: require("./signalr.html")
        };
    });
};