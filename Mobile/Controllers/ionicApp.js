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
                  console.log($stateParams.CategoryID);
                  for (i = 0; i < $scope.categories.length; i++) {
                      console.log($scope.categories[i].CategoryID);
                      if ($stateParams.CategoryID == $scope.categories[i].CategoryID) {
                          $scope.category = $scope.categories[i];
                          break;
                      }
                  }
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
.controller('MyCtrl', function ($scope, $http, $ionicModal, $state, $rootScope, $ionicPopup, $ionicActionSheet) {

    $scope.data = {
        showDelete: false
    };

    $rootScope.$on('$stateChangeSuccess',
    function (event, toState, toParams, fromState, fromParams) {
        $scope.statename = $state.current.name;
    });

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

    $scope.onItemDelete = function (item) {
        $scope.showConfirm(item);
        //if ($scope.showConfirm('Item',item)) {
         //   console.log("Delete confirmed: " + item);
         //   $scope.items.splice($scope.items.indexOf(item), 1);
         //}
        
    };

    getCategories();

    function getCategories() {
        $http.get('http://localhost:2478/api/Category')
     .then(function (res) {
         console.log(res);
         //$scope.people = res.data;
         $scope.categories = res.data;
         $scope.$broadcast('scroll.refreshComplete');
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

    // ion-modal


    $ionicModal.fromTemplateUrl('category.create.html', {
        scope: $scope,
        animation: 'slide-in-up',
        focusFirstInput: true
    }).then(function (modal) {
        $scope.categoryModal = modal;
        //$scope.modal.show();
    });

    $ionicModal.fromTemplateUrl('subcategory.create.html', {
        scope: $scope,
        animation: 'slide-in-up',
        focusFirstInput: true
    }).then(function (modal) {
        $scope.subCategoryModal = modal;
        //$scope.modal.show();
    });


    $ionicModal.fromTemplateUrl('item.create.html', {
        scope: $scope,
        animation: 'slide-in-up',
        focusFirstInput: true
    }).then(function (modal) {
        $scope.itemModal = modal;
        //$scope.modal.show();
    });

    $scope.openModal = function () {
        //$scope.statename = $state.current.name;
        if ($scope.statename == 'item')
            $scope.itemModal.show();
        else if ($scope.statename == 'subcategory')
            $scope.subCategoryModal.show();
        else if ($scope.statename == 'category')
            $scope.categoryModal.show();
        else
            $scope.show();
    };
    $scope.closeModal = function () {
        if ($scope.statename == 'item')
            $scope.itemModal.hide();
        else if ($scope.statename == 'subcategory')
            $scope.subCategoryModal.hide();
        else
            $scope.categoryModal.hide();
    };
    //// Cleanup the modal when we're done with it!
    //$scope.$on('$destroy', function () {
    //    $scope.modal.remove();
    //});
    //// Execute action on hide modal
    //$scope.$on('modal.hidden', function () {
    //    // Execute action
    //});
    //// Execute action on remove modal
    //$scope.$on('modal.removed', function () {
    //    // Execute action
    //});

    $scope.createCategory = function (newCategoryName,newCategoryType) {
        var data = {
            Name: newCategoryName,
            CategoryType: newCategoryType
        };
        console.log(JSON.stringify({ data: data }));

        var res = $http.post('http://localhost:2478/api/category/create', data);
        res.success(function (data, status, headers, config) {
            $scope.message = data;
            console.log("success message: " + JSON.stringify({ data: data }));
            $scope.showAlert('Success', 'Category is Saved');
            $scope.categoryModal.hide();
            getCategories();
        });
        res.error(function (data, status, headers, config) {
            console.log("failure message: " + JSON.stringify({ data: data }));
        });

    };

    $scope.createSubCategory = function (newSubCategoryName) {
        var data = {
            Name: newSubCategoryName,
            CategoryID: $scope.SelectedCategoryID
        };
        console.log(JSON.stringify({ data: data }));

        var res = $http.post('http://localhost:2478/api/subcategory/create', data);
        res.success(function (data, status, headers, config) {
            $scope.message = data;
            console.log("success message: " + JSON.stringify({ data: data }));
            $scope.showAlert('Success', 'SubCategory is Saved');
            $scope.subCategoryModal.hide();
            getSubCategories();
        });
        res.error(function (data, status, headers, config) {
            console.log("failure message: " + JSON.stringify({ data: data }));
        });

    };

    $scope.createItem = function (newItemDate, newItemAmount) {
        var data = {
            Date: newItemDate,
            SubCategoryID: $scope.SelectedSubCategoryID,
            Amount: newItemAmount
        }

        console.log(JSON.stringify({ data: data }));

        var res = $http.post('http://localhost:2478/api/item/create', data);
        res.success(function (data, status, headers, config) {
            $scope.message = data;
            console.log("success message: " + JSON.stringify({ data: data }));
            $scope.showAlert('Success', 'Item is Saved');
            $scope.itemModal.hide();
            getItems();
        });
        res.error(function (data, status, headers, config) {
            console.log("failure message: " + JSON.stringify({ data: data }));
        });

    };

    $scope.CategorySelectedChanged = function (category) {
        console.log("CategorySelectedChanged: " + category.CategoryID);
        $scope.SelectedCategoryID = category.CategoryID;
    }

    $scope.SubCategorySelectedChanged = function (subCategory) {
        console.log("SubCategorySelectedChanged: " + subCategory.SubCategoryID);
        $scope.SelectedSubCategoryID = subCategory.SubCategoryID;
    }

    $scope.showAlert = function (title,template) {
        var alertPopup = $ionicPopup.alert({
            title: title,
            template: template
        });

        alertPopup.then(function (res) {
            console.log(res);
        });
    };

    $scope.showConfirm = function (item) {
        var confirmPopup = $ionicPopup.confirm({
            title: 'Warning',
            template: 'Are you sure you want to delete it?'
        });
        confirmPopup.then(function (res) {
            if (res) {
                console.log(item + ' will be deleted!');
                switch ($scope.statename)
                {
                    case 'item':
                        $scope.items.splice($scope.items.indexOf(item), 1);
                        break;
                    case 'subcategory':
                        $scope.subCategories.splice($scope.subCategories.indexOf(item), 1);
                        break;
                    case 'category':
                        $scope.categories.splice($scope.categories.indexOf(item), 1);
                        break;
                    default:
                        break;
                }
            } else {
                console.log('You are not sure');
            }
        });
    };

    $scope.show = function () {

        // Show the action sheet
        var hideSheet = $ionicActionSheet.show({
            buttons: [
              { text: '<b>Share</b> This' },
              { text: 'Move' }
            ],
            destructiveText: 'Delete',
            titleText: 'Modify your album',
            cancelText: 'Cancel',
            cancel: function () {
                // add cancel code..
                console.log('Action Sheet is cancelled');
            },
            buttonClicked: function (index) {
                console.log("buttonClicked" + index);
                return true;
            },
            destructiveButtonClicked: function () {
                console.log("destructiveButtonClicked");
                return true;
            }
        });

    };

    $scope.doRefreshCategory = function () {
        getCategories();
        //$http.get('http://localhost:2478/api/Category')
        // .then(function (newItems) {
        //     console.log("on refresh success: ");
        //     $scope.categories = newItems.data;
        // })
        // .finally(function() {
        // //    // Stop the ion-refresher from spinning
        //     $scope.$broadcast('scroll.refreshComplete');
        // })
        //;
    };

    chart();

    function chart() {
        $http.get('http://localhost:2478/api/item/chart')
        .then(function (res) {
            console.log(res);
            //$scope.people = res.data;
            $scope.chart = res.data;
            BarChart(res.data);
        });
    };

    function BarChart(data) {
        console.log("BarChart: " + data.lstMonth);
        console.log("BarChart: " + data.lstExpences);
        console.log("BarChart: " + data.lstIncome);
        var barChartData = {
            labels: data.lstMonth,
            datasets: [
                {
                    label: "Giderler",
                    backgroundColor: "rgba(255, 0, 0, 0.4)",
                    borderColor: "rgba(255, 0, 0, 1)",
                    borderWidth: 1,
                    hoverBackgroundColor: "rgba(255, 0, 0, 0.6)",
                    hoverBorderColor: "rgba(255, 0, 0, 1)",
                    data: data.lstExpences
                },
                {
                    label: "Gelirler",
                    backgroundColor: "rgba(0, 175, 0, 0.4)",
                    borderColor: "rgba(0, 175, 0, 1)",
                    borderWidth: 1,
                    hoverBackgroundColor: "rgba(0, 175, 0, 0.6)",
                    hoverBorderColor: "rgba(0, 175, 0, 1)",
                    data: data.lstIncome
                }
            ]

        }
        var ctx = document.getElementById("dvbarchart").getContext("2d");
        //var ctx = $scope.dvbarchart.getContext("2d");

        window.myBar = new Chart(ctx, {
            type: 'bar',
            data: barChartData,
            options: { responsive: true }

        });
    }
})
;