var app = angular.module('app', []);

app.controller('MainCtrl', function ($scope, $http) {
    $scope.name = 'World';
    $scope.show = false;

    $scope.getValues = function () {
        $http.get("/api/values").then(function (response) {
            $scope.show = true;
            $scope.values = response.data;
        });
    }
});
