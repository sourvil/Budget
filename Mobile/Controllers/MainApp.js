angular.module('mainApp', ['ngRoute', 'ui.router'])
.config(['$urlRouterProvider', '$stateProvider',
  function ($urlRouterProvider, $stateProvider) {

      $stateProvider
          .state("index", {

             // Use a url of "/" to set a states as the "index".
              url: "/index",
             templateUrl: '/index.html'

          })
          .state("item", {

              // Use a url of "/" to set a states as the "index".
              url: "/item",
              templateUrl: '/item.html'

          })
          .state("category", {

              // Use a url of "/" to set a states as the "index".
              url: "/category",
              templateUrl: '/category.html'

          })
                .state("subcategory", {

                    // Use a url of "/" to set a states as the "index".
                    url: "/subcategory",
                    templateUrl: '/subcategory.html'

                })

      $urlRouterProvider.when('', '/index');

  }])
.run([function () {
    /* Run is when the app gets kicked off*/
    console.log("Run hook");
}])

.controller("MainCtrl", function ($scope, $http) {
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

    getItems();

    function getItems() {
        $http.get('http://localhost:2478/api/Item')
     .then(function (res) {
         console.log(res);
         //$scope.people = res.data;
         $scope.items = res.data;
     });
    }

    getSubCategories();

    function getSubCategories() {
        $http.get('http://localhost:2478/api/SubCategory')
     .then(function (res) {
         console.log(res);
         //$scope.people = res.data;
         $scope.subCategories = res.data;
     });
    }

});