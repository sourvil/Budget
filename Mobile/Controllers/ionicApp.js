angular.module('ionicApp', ['ionic', 'ui.router'])
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

          .state('categorydetail', {
              url: '/categorydetail/:CategoryID',
              // loaded into ui-view of parent's template
              templateUrl: 'category.detail.html',
              controller: function ($scope, $stateParams) {
                  console.log("category_detail scope");
                  $scope.category = $scope.categories[$stateParams.CategoryID];
              },
              onEnter: function () {
                  console.log("enter category.detail");
              }
          })

          .state("subcategory", {

              // Use a url of "/" to set a states as the "index".
              url: "/subcategory",
              templateUrl: '/subcategory.html'

          })
                .state("home", {

                    // Use a url of "/" to set a states as the "index".
                    url: "/index",
                    templateUrl: '/index.html'

                })

      $urlRouterProvider.when('', '/index');

  }])
.controller('MyCtrl', function ($scope, $http) {

    $scope.data = {
        showDelete: false
    };

    //$scope.edit = function (item) {
    //    alert('Edit Item: ' + item.id);
    //};
    //$scope.share = function (item) {
    //    alert('Share Item: ' + item.id);
    //};

    //$scope.moveItem = function (item, fromIndex, toIndex) {
    //    $scope.items.splice(fromIndex, 1);
    //    $scope.items.splice(toIndex, 0, item);
    //};

    //$scope.onItemDelete = function (item) {
    //    $scope.items.splice($scope.items.indexOf(item), 1);
    //};

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