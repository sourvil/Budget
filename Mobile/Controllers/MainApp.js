﻿angular.module("MainApp", [])

.controller("MainController", function ($scope, $http) {
    //$scope.surname = "donbay";
    //$scope._surname2 = "donbay2";
    //$scope.teams = [{ 'name': "Fenerbahçe", 'colors': 'Yellow-Navy' }, { 'name': 'Galatasaray', 'colors': 'Yellow-Red' }, {'name':'Beşiktaş','colors' : 'White-Black'}];

    //function setSurname(surname)
    //{
    //    console.log("SetSurname is called");
    //    console.log(surname);
    //};

    //$scope.setSurname = setSurname;

    $scope.changeView = function (view) {
        alert(view);
        $location.path(view); // path not hash
    }

    getCategories();

    function getCategories() {
        $http.get('http://localhost:2478/api/Category')
     .then(function (res) {
         console.log(res);
         //$scope.people = res.data;
         $scope.categories = res.data;
     });
    }

});