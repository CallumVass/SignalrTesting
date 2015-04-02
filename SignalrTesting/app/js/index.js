require("angular");
require("angular-ui-router");

var ngModule = angular.module("app", ["ui.router"]);

ngModule.config(function ($stateProvider, $urlRouterProvider) {

    $stateProvider
        .state('home', {
            url: '/',
            template: '<signalr></signalr>'
        });

    $urlRouterProvider.otherwise('/');
});

require("./directives/index")(ngModule);