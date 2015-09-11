
deps.push('ui.tree');

app.controller("SecMenuController", function ($scope, $http, Erpmodal) {

    $scope.Modules = [];
    $scope.SelectedModule = {};
    $scope.SaveDisabled = true;
    var NormalMenu = [];

    $scope.ResourceTree = [];

    function createTreeMap(parentId, list) {
        var nodes = [];
        for (var i = 0, l; l = list[i]; i++) {
            if (l.SecResourcesId === parentId) {
                // list.splice(i, 1); i--;
                nodes.push({
                    Id: l.Id
                  , SecResourcesId: l.SecResourcesId
                  , DisplayName: l.DisplayName
                  , SerialNo: l.SerialNo
                  , items: createTreeMap(l.Id, list)
                });
            }
        }
        return nodes;
    }//end of createTreeMap

    var init = function () {

        $http.get("/Security/Module/GetByCompanyId").success(function (data) {
            $scope.Modules = data;
        }).error(function (data) {

        });


    };
    init();

    $scope.GetResourceByModuleId = function () {

        $http.get("/Security/Menu/GetMenuByModuleId?secModuleId=" + $scope.SelectedModule).success(function (data) {
            NormalMenu = data;
            NormalMenu = _.sortBy(NormalMenu, function (item) {
                return item.SerialNo;
            });
            $scope.ResourceTree = createTreeMap(null, NormalMenu);
            $scope.SaveDisabled = false;
        }).error(function (data) {

        });

    };// end of GetResourceByModuleId
    $scope.Reset = function () {

        $scope.SaveDisabled = true;
        $scope.SelectedModule = {};
        $scope.ResourceTree = {};

    }//end of Reset
    $scope.Save = function () {
        Erpmodal.Confirm('Save').then(function (result) {
            $http.post("/Security/Menu/UpdateMenu", { objMenus: $scope.ResourceTree }).success(function (data) {

                Erpmodal.Save(data, "Save");
                //$scope.Reset();

            }).error(function (data) {

            });
        });

    };// end of Save

   


});