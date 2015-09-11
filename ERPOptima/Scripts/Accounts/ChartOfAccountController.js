deps.push('angularBootstrapNavTree');

if (angular.version.full.indexOf("1.2") >= 0) {
    deps.push('ngAnimate');
}

(function () {
    app.controller('chartOfAccountController', function ($scope, $timeout, $http, Erpmodal, $validation) {
        var state;

        var current, rawdata, currentObject;
        var parents = [];

        var init = function () {
            state = 0;
            current = null;
            rawdata = null;
            currentObject = null;

            $scope.ShowLoading = false;
            $scope.ButtonNewDisabled = false;

            $scope.ButtonDeleteDisabled = true;
            // $scope.ButtonSaveDisabled = true;
            $scope.ScheduleNoDisabled = true;
            $scope.TransactionalHeadDisabled = true;
            $scope.Status = true;
            //for checkboxes

            $scope.selected = false;//for current selection

            $scope.AnFChartOfAccount = {};
            $scope.AnFChartOfAccount.information = {};
            $scope.AnFChartOfAccount.parents;
            $scope.AnFChartOfAccount.childrens;

            $scope.my_data = [];

            parents = [];

            $scope.ShowLoading = true;
            $http.get("/Accounts/ChartOfAccount/GetChartOfAccount").success(function (data) {
                $scope.ShowLoading = false;
                rawdata = _.sortBy(data, function (item) {
                    return item.AnFChartOfAccountId;
                });

                $scope.my_data = createTreeMap(null, data);
            });
        }//end of init

        init();//init is called

        function createTreeMap(parentId, list) {
            var nodes = [];
            for (var i = 0, l; l = list[i]; i++) {
                if (l.AnFChartOfAccountId === parentId) {
                    // list.splice(i, 1); i--;
                    nodes.push({
                        Id: l.Id
                      , AnFChartOfAccountId: l.AnFChartOfAccountId
                      , Name: l.Name
                      , Code: l.Code
                      , Name: l.Name
                      , CmnCompanyId: l.CmnCompanyId
                      , ScheduleNo: l.ScheduleNo
                      , DepthLevel: l.DepthLevel
                      , IsTransactionalHead: l.IsTransactionalHead
                      , Status: l.Status
                      , children: createTreeMap(l.Id, list)
                    });
                }
            }
            return nodes;
        }//end of findChildren

        function findParent(parentId, list) {
            for (var i = 0, l; l = list[i]; i++) {
                if (l.Id === parentId) {
                    parents.push({
                        Id: l.Id
                     , AnFChartOfAccountId: l.AnFChartOfAccountId
                     , Name: l.Name
                     , Code: l.Code
                    }
                    );

                    findParent(l.AnFChartOfAccountId, list);
                }
            }
        }//end of findParent

        $scope.my_tree_handler = function (branch) {
            $scope.selected = true;
            $scope.ButtonDeleteDisabled = false;
            current = branch;

            $scope.AnFChartOfAccount.information = {
                Id: branch.Id
                          , AnFChartOfAccountId: branch.AnFChartOfAccountId
                          , Name: branch.Name
                          , Code: branch.Code
                          , Name: branch.Name
                          , CmnCompanyId: branch.CmnCompanyId
                          , ScheduleNo: branch.ScheduleNo
                          , DepthLevel: branch.DepthLevel
                          , IsTransactionalHead: branch.IsTransactionalHead
                          , Status: branch.Status
            };

            $scope.ButtonNewDisabled = $scope.AnFChartOfAccount.information.DepthLevel == 6 || $scope.AnFChartOfAccount.information.IsTransactionalHead == true ? true : false;

            $scope.ScheduleNoDisabled = current.DepthLevel == 3 ? false : true;

            $scope.AnFChartOfAccount.childrens = _.where(branch.children, { AnFChartOfAccountId: branch.Id });

            $scope.TransactionalHeadDisabled = $scope.AnFChartOfAccount.IsTransactionalHead == true || ($scope.AnFChartOfAccount.childrens.length == 0 && current.DepthLevel > 3) ? false : true;

            parents = [];
            findParent(branch.AnFChartOfAccountId, rawdata);
            $scope.AnFChartOfAccount.parents = _.sortBy(parents, function (item) {
                return item.Id;
            });
        };//end of tree_handler

        $scope.Save = function () {
            
            Erpmodal.Confirm('Save').then(function (result) {

                if (state == 1) {
                    $scope.AnFChartOfAccount.information.AnFChartOfAccountId = current.Id;
                    $scope.AnFChartOfAccount.information.DepthLevel = current.DepthLevel + 1;
                    $scope.AnFChartOfAccount.information.CmnCompanyId = current.CmnCompanyId;
                }

                $http.post("/Accounts/ChartOfAccount/SaveChartOfAccount", $scope.AnFChartOfAccount.information).success(

                    function (data) {
                        Erpmodal.Save(data, "Save");
                        $scope.Reset();
                        current.childrens.push($scope.AnFChartOfAccount.information);
                       // init();
                        $validation.reset($scope.Account);
                    }

                    ).error(function (error) {
                    });
            });
            
        };//end of Save

        $scope.PrepareCode = function () {
            var childCode = _.sortBy(current.children, function (item) {
                return item.Code;
            }).reverse();

            var codeGenerationViewModel = new Object(); // viewmodel object

            codeGenerationViewModel.ParentCode = current.Code;
            codeGenerationViewModel.ChildCode = childCode.length > 0 ? childCode[0].Code : "";
            codeGenerationViewModel.Level = current.DepthLevel;

            if (codeGenerationViewModel.ChildCode != "") {
                if (codeGenerationViewModel.ChildCode.length != 12) {
                    $scope.AnFChartOfAccount.information.IsTransactionalHead = false;

                    codeGenerationViewModel.IsLastNode = false;
                }
                else {
                    $scope.AnFChartOfAccount.information.IsTransactionalHead = true;
                    codeGenerationViewModel.IsLastNode = true;
                }
            }

            else {
                if ($scope.AnFChartOfAccount.information.IsTransactionalHead) {
                    codeGenerationViewModel.ChildCode = "";
                    codeGenerationViewModel.IsLastNode = true;
                }

                else {
                    codeGenerationViewModel.ChildCode = "";
                    codeGenerationViewModel.IsLastNode = false;
                }
            }

            $http.post("/Accounts/ChartOfAccount/GenerateChartOfAccountChildCode", codeGenerationViewModel).success(

                function (data) {
                    currentObject = $scope.AnFChartOfAccount.information;

                    $scope.AnFChartOfAccount.information.Code = data;

                    $scope.TransactionalHeadDisabled = true;
                }

                ).error(function (data) {
                });
        };

        $scope.New = function () {
           
                state = 1;// now in New Mode

                $scope.ButtonNewDisabled = true;
                $scope.ButtonDeleteDisabled = true;

                //$scope.ScheduleNoDisabled = true;

                // $scope.TransactionalHeadDisabled = true;

                $scope.ScheduleNoDisabled = current.DepthLevel + 1 == 3 ? false : true;

                $scope.TransactionalHeadDisabled = current.DepthLevel + 1 > 3 ? false : true;

                $scope.AnFChartOfAccount.information = {};//reset the form

                $scope.PrepareCode();
            
        }//end of New

        $scope.Reset = function () {
            init();
            $scope.Account.$setPristine();
            $validation.reset($scope.Account);
        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {

                var Id = current.Id;
                $http.post("/Accounts/ChartOfAccount/DeleteChartOfAccount", { Id: Id }).success(function (data) {
                    Erpmodal.Delete(data,"Delete");
                    init();
                    $validation.reset($scope.Account);
                }).error(function () {

                });// end of http
            });// end of then
        };// end of delete
    });
}).call(this);