const $ = require('jquery');

// most jQuery plugins don't work unless jQuery is on the window as well :-/
window.$ = window.jQuery = $;

require("angular");
require("angular-ui-router");

const ngModule = angular.module("app", ["ui.router"]);

ngModule.config(function ($stateProvider, $urlRouterProvider) {

    $stateProvider
        .state('home', {
            url: '/',
            template: '<signalr></signalr>'
        });

    $urlRouterProvider.otherwise('/');
});

require("./config")(ngModule);
require("./services")(ngModule);
require("./directives")(ngModule);