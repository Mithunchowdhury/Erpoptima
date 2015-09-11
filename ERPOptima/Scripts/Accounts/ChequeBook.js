


$(document).ready(function () {
    $('.datetimepicker').datetimepicker();

  
    
});

(function () {


    app.controller('chequeBook', function ($scope, ngTableParams, $http, Erpmodal) {


        var current;
        var chequeBookId=0;
        var state;
        var datas = [];
        $scope.AccountNameAndCode = new Object();
        $scope.ChequeBook = new Object();
        $scope.accountName;
        var init = function () {

            state = 0;
            $http({
                url: '/Accounts/ChequeBook/GetTransactionalHeadForChequeBook',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {                
                $scope.AccountNameAndCode = data;
            });
        }//end of init

        $scope.tableParams = new ngTableParams({
            page: 1,            // show first page
            count: 10           // count per page
        }, {
            total: datas.length, // length of data
            getData: function ($defer, params) {
                $defer.resolve(datas.slice((params.page() - 1) * params.count(), params.page() * params.count()));

            }
        });
        $scope.loadCheckBookInfo = function () {            
            var anfId = $scope.accountName;
            $http({
                url: '/Accounts/ChequeBook/GetChequeBooks?anfCOAId=' + anfId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {               
                datas = [];
                for (var i = 0; i < data.length; i++) {
                    data[i].IssueDate = ConverttoDateString(data[i].IssueDate);                   
                }                
                datas = data;
                $scope.tableParams.reload();
            });
        }
        init();//init is called

        $scope.setFortEdit = function (index) {

            var pid = datas[index];
            current = index;
            state = 1;
            chequeBookId = pid.Id;
            $scope.ChequeBook.ChequeBookNo = pid.ChequeBookNo;
            $scope.ChequeBook.IssueDate = pid.IssueDate;
            $scope.ChequeBook.NoofPage = pid.NoofPage;
            $scope.ChequeBook.StartingPageNo = pid.StartingPageNo;
            $scope.ChequeBook.EndingPageNo = pid.EndingPageNo;
            $scope.ChequeBook.AnFChartOfAccountId = pid.AnFChartOfAccountId;
            $scope.ChequeBook.CreatedBy = pid.CreatedBy;
            $scope.ChequeBook.CreatedDate = ConverttoDateString(pid.CreatedDate);

          
        }
       
        $scope.Save = function () {
            Erpmodal.Confirm('Save').then(function (result) {
                if (state == 1) {
                    $scope.ChequeBook.Id = chequeBookId;
                }
                else { $scope.ChequeBook.Id = 0; }
                $scope.ChequeBook.AnFChartOfAccountId = $scope.accountName;
                $http.post("/Accounts/ChequeBook/SaveChequeBook", $scope.ChequeBook).success(
                    function (data) {
                        // init();
                        chequeBookId = 0;
                        // $scope.loadCheckBookInfo();
                        if (state != 1) {
                            $scope.ChequeBook.Id = IdId;
                            datas.push($scope.ChequeBook);
                        }
                        else {
                            datas[current] = $scope.ChequeBook;
                        }
                        state = 0;
                        current = {};
                        $scope.tableParams.reload();
                        $scope.ChequeBook = {};
                        Erpmodal.Save(data, "Save");
                        $scope.Reset();
                    }
                    ).error(function (error) {
                    });
            });

        };//end of Save

        $scope.Reset = function () {
            chequeBookId = 0;
            state = 0;
            $scope.ChequeBook = {};
            current = {};
            $scope.Cheque.$setPristine();
        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {
                var Id = chequeBookId;
                if (chequeBookId != 0) {
                    $http.post("/Accounts/ChequeBook/DeleteChequeBook", { Id: Id }).success(function (data) {
                        datas.splice(current);
                        chequeBookId = 0;
                        state = 0;
                        current = {};
                        $scope.tableParams.reload();
                        $scope.ChequeBook = {};
                        Erpmodal.Delete(data, "Delete");

                    }).error(function () {

                    })
                }
            });
        };
    });

}).call(this);