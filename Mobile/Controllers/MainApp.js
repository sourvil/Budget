angular.module("MainApp", [])

.controller("MainController", function ($scope, $http) {
    $scope.surname = "donbay";
    $scope._surname2 = "donbay2";
    $scope.teams = [{ 'name': "Fenerbahçe", 'colors': 'Yellow-Navy' }, { 'name': 'Galatasaray', 'colors': 'Yellow-Red' }, {'name':'Beşiktaş','colors' : 'White-Black'}];

    function setSurname(surname)
    {
        console.log("SetSurname is called");
        console.log(surname);
    };

    $scope.setSurname = setSurname;

});

