(function (angular) {
    'use strict';
    var myApp = angular.module('logCentralApp', ['ngRoute']);

    myApp.config(['$routeProvider',
        function ($routeProvider) {
            $routeProvider.
                when('/Login', {
                    templateUrl: '/Login.html'
                }).
                when("/Register", {
                    templateUrl: '/Register.html'
                }).
                when("/Logs", {
                    templateUrl: '/Logs.html'
                }).
                when("/LogDetail", {
                    templateUrl: '/LogDetail.html'
                }).
                otherwise({
                    redirectTo: '/Login'
                });;
        }]);

    myApp.controller('LoginController', ['$scope', '$window', '$http', '$rootScope', function ($scope, $window, $http, $rootScope) {
        $scope.login = function () {
            $http({
                method: 'POST',
                url: 'https://localhost:44346/api/Users/Authenticate',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: JSON.stringify($scope.user)
            }).then(function successCallback(response) {
                $scope.user.token = response.data;
                sessionStorage.user = angular.toJson($scope.user);
                $window.location = '#!/Logs';
            }, function errorCallback(response) {
                alert('erro!');
            });
        }
    }]);

    myApp.controller('RegisterController', ['$scope', '$window', '$http', function ($scope, $window, $http) {
        $scope.register = function () {
            $scope.user.nome = $scope.user.email;

            $http({
                method: 'POST',
                url: 'https://localhost:44346/api/Users/Register',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: JSON.stringify($scope.user)
            }).then(function successCallback(response) {
                alert('Usuário cadastrado com sucesso!');
                $window.location = '#!/Login';
            }, function errorCallback(response) {
                alert('erro!');
            });
        }
    }]);

    myApp.controller('LogsController', ['$scope', '$window', '$http', function ($scope, $window, $http) {
        $scope.user = angular.fromJson(sessionStorage.user);
        $scope.filters = { environment: null, sort: null, filterType: null, filterText: '' }
        $scope.pageSize = 15;
        $scope.currentPage = 0;

        if ($scope.user == undefined)
            $window.location = '#!/Login';

        $scope.getLogs = function (page, $event) {

            if ($event != undefined)
                $event.preventDefault();

            if (page < 0 || page >= $scope.totalPages)
                return

            $http({
                method: 'GET',
                url: `https://localhost:44346/api/Logs?PageSize=${$scope.pageSize}&PageIndex=${page}`,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + $scope.user.token
                },
            }).then(function successCallback(response) {
                if (response.data.isSuccess) {
                    $scope.currentPage = page;
                    $scope.logs = response.data.value;
                    $scope.totalItems = response.data.totalItems;
                    $scope.totalPages = Math.ceil(response.data.totalItems / $scope.pageSize);
                }
                else {
                    alert('erro!');
                }
            }, function errorCallback(response) {
                alert('erro!');
            });
        }

        $scope.deleteLogs = function () {
            var logsTodelete = $scope.logs.filter(function (log) { return log.selected });

            angular.forEach(logsTodelete, function (log) {
                $http({
                    method: 'DELETE',
                    url: `https://localhost:44346/api/Logs?logId=${log.id}`,
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer ' + $scope.user.token
                    },
                }).then(function successCallback(response) {
                    $scope.logs = $scope.logs.filter(function (l) { return l.id != log.id });
                }, function errorCallback(response) {
                    alert('erro!');
                });
            });
        }

        $scope.logoff = function () {
            sessionStorage.removeItem('user');
            $window.location = '#!/Login';
        }

        $scope.getLogs(0);
    }]);

    myApp.controller('LogDetailController', ['$scope', '$window', '$http', '$location', function ($scope, $window, $http, $location) {
        $scope.user = angular.fromJson(sessionStorage.user);

        if ($scope.user == undefined)
            $window.location = '#!/Login';

        $scope.getLog = function (id) {
            $http({
                method: 'GET',
                url: `https://localhost:44346/api/Logs/${id}`,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + $scope.user.token
                },
            }).then(function successCallback(response) {
                $scope.log = response.data.value;
            }, function errorCallback(response) {
                alert('erro!');
            });
        }

        var searchObject = $location.search();

        if (searchObject.id == undefined)
            $window.location = '#!/Logs';
        else
            $scope.getLog(searchObject.id);
    }]);

})(window.angular);
