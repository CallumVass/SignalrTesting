require("signalr");

module.exports = function (ngModule) {
    ngModule.directive("signalr", function () {
        return {
            restrict: "E",
            template: "<div>Hello There</div>"
        };
    });
};