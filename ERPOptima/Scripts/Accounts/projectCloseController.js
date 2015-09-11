


$(document).ready(function () {
    $('.datetimepicker').datetimepicker();



});

(function () {


    app.controller('ProjectClose', function ($scope, ngTableParams, $http, Erpmodal) {

        var currentId;
        var currentName;
        var currentCode;
        var chequeBookId = 0;
        $scope.BusinessId = 0;

        var datas = [];
        $scope.Businesses = new Object();
        $scope.Project = new Object();

        $scope.Project = {};

        $scope.transformStatus = function (closingStatus) {

            var result = "";
            if (closingStatus == 0) {
                result = "Open";
            }
            else if (closingStatus == 1) {
                result = "Optional Close";
            }
            else if (closingStatus == 2) {
                result = "Close";
            }
            return result;

        };
      
        var init = function () {
            $scope.SaveDisabled = true;

            state = 0;
            $http({
                url: '/Accounts/ProjectClose/GetBusinesses',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
               
                $scope.Businesses = data;
                $scope.BusinessId = 0;
            });

        }//end of init
        $scope.ProjectInfo = function () {
            $http({
                url: '/Accounts/ProjectClose/GetProjects?businessId=' + $scope.BusinessId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].StartDate != null && data[i].EndDate != null) {
                        data[i].StartDate = ConverttoDateString(data[i].StartDate);
                        data[i].EndDate = ConverttoDateString(data[i].EndDate);
                    }
                }
                $scope.Projects = data;
                datas = data;
                $scope.tableParams.reload();
            });
        };
        $scope.tableParams = new ngTableParams({
            page: 1,            // show first page
            count: 10           // count per page
        }, {
            total: datas.length, // length of data
            getData: function ($defer, params) {
                $defer.resolve(datas.slice((params.page() - 1) * params.count(), params.page() * params.count()));

            }
        });
       
        init();//init is called

        $scope.setFortEdit = function (pid) {
            $scope.SaveDisabled = false;

            currentId = pid.Id;
            currentName = pid.Name;
            currentCode = pid.Code;
            $scope.Project.Name = pid.Name;
            $scope.Project.ClosingStatus = pid.ClosingStatus;
            $scope.Project.ClosingNote = pid.ClosingNote;
            $scope.Project.CreatedBy = pid.CreatedBy;
            $scope.Project.CreatedDate = ConverttoDateString(pid.CreatedDate);

            //$scope.Project.ProjectId = pid.Id;
        }

        $scope.Update = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                $scope.Project.Id = currentId;
                $scope.Project.Name = currentName;
                $scope.Project.Code = currentCode;
                $http.post("/Accounts/ProjectClose/UpdateCmnProject", $scope.Project).success(
                    function (data) {
                        $scope.Project = {};
                        currentId = 0;
                        currentName = '';
                        currentCode = '';
                        $scope.ProjectInfo();
                        $scope.tableParams.reload();
                        Erpmodal.Save(data, "Save");
                        $scope.SaveDisabled = true;

                    }
                    ).error(function (error) {
                    });
            });

        };//end of Save

        $scope.Reset = function () {
            $scope.Project = {};
            currentId = 0;
            currentName = '';
            currentCode ='';
            $scope.SaveDisabled = true;
        }//end of Reset

       
    });

}).call(this);