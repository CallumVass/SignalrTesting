export default ngModule => {
    ngModule.directive("barChart", function ($window) {
        return {
            restrict: 'E',
            transclude: true,
            scope: {
                data: '='
            },
            template: "<div />",
            controller: function ($scope, $timeout) {

                $timeout(function () {
                    // simulate data updating
                    $scope.chart.load({
                        columns: [
                            ['data3', 30]
                        ]
                    });
                }, 2000);
            },
            link: {
                pre: function ($scope, $element) {
                    $scope.chart = null;
                },
                post: function ($scope, $element) {
                    if (!$scope.chart) {
                        $scope.chart = c3.generate({
                            bindto: $element[0],
                            data: {
                                columns: [
                                    ['data1', 30],
                                    ['data2', 90],
                                    ['data3', 60]
                                ],
                                type: 'bar',
                                groups: [
                                    ['data1', 'data2', 'data3', 'data4']
                                ]
                            },
                            axis: {
                                rotated: true,
                                x: {
                                    show: false
                                },
                                y: {
                                    show: false
                                }
                            },
                            size: {
                                height: 30,
                                width: $element[0].parentElement.clientWidth - 20
                            },
                            legend: {
                                show: false
                            },
                            grid: {
                                x: {
                                    show: false
                                },
                                y: {
                                    show: false
                                }
                            }
                        });
                    }

                    $scope.$on('$destroy', function () {
                        $scope.chart = $scope.chart.destroy();
                    });

                    var w = angular.element($window);
                    w.bind('resize', function () {
                        $scope.chart.resize({width:$element[0].parentElement.clientWidth - 20, height: 30})
                        $scope.$apply();
                    });
                }
            }
        }
    });
};