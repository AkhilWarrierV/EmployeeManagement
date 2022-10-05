$(document).ready(function () {
    bindEvents();
    hideEmployeeDetailCard();
});

function bindEvents() {
    $(".employeeDetails").on("click", function (event) {
        var employeeId = event.currentTarget.getAttribute("data-id");
        

        $.ajax({
            url: 'https://localhost:44383/api/internal/employee/' + employeeId,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                var newEmployeeCard = `<div class="card">
                                          <h1>${result.name}</h1>
                                         <b>Id :</b> <p>${result.id}</p>
                                         <b>Department:</b><p>${result.department}</p>
                                         <b>Age:</b><p>${result.age}</p>
                                         <b>Address:</b><p>${result.address}</p>
                                        </div>`

                $("#EmployeeCard").html(newEmployeeCard);
                showEmployeeDetailCard();
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    $(".employeeDelete").on("click", function (event) {
        var employeeId = event.currentTarget.getAttribute("data-id");
        if (confirm('Are you sure you want to delete')) {
            $.ajax({
                url: 'https://localhost:44383/api/internal/employee/deleteEmployees/' + employeeId,
                type: 'DELETE',
                contentType: "application/json; charset=utf-8",
                success: function (result) {

                    location.reload();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }

    });

    $("#createform").submit( function (event) {
        
        var employeeDetailedViewModel = {};
       
        employeeDetailedViewModel.Name = $("#name").val();
        employeeDetailedViewModel.Department = $("#dept").val();
        employeeDetailedViewModel.Age = Number($("#age").val());
        employeeDetailedViewModel.Address = $("#address").val();
        
        var data = JSON.stringify(employeeDetailedViewModel);
        
        $.ajax({
            url: 'https://localhost:44383/api/internal/employee/insert-employee',
            type: 'POST',
          contentType: "application/json; charset=utf-8",
            data: data,
            async: false,
            success: function (result)
            {
                    location.reload();
          
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    $(".employeeEdit").on("click", function (event) {
        console.log("clicked");
        var employeeId = event.currentTarget.getAttribute("data-id");


        $.ajax({
            url: 'https://localhost:44383/api/internal/employee/' + employeeId,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                $("#updateid").val(result.id)
                $("#updatename").val(result.name)
                $("#updatedept").val(result.department)
                $("#updateage").val(result.age)
                $("#updateaddress").val(result.address)
            },
            error: function (error) {
                console.log(error);
            }
        });
        $("#updateform").submit(function (event) {
            console.log("clicked");
            var idUpdate = $("#updateid").val();
            var nameUpdate = $("#updatename").val();
            var departmentUpdate = $("#updatedept").val();
            var ageUpdate = $("#updateage").val();
            var addressUpdate = $("#updateaddress").val();

            let employees = {
                id: parseInt(idUpdate),
                name: nameUpdate,
                department: departmentUpdate,
                age: parseInt(ageUpdate),
                address: addressUpdate
            };
            $.ajax({
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                url: 'https://localhost:44383/api/internal/employee/update-employee',
                type: 'PUT',
                data: JSON.stringify(employees),
                dataType: 'json',
                async: false,
                success: function (result)
                {
                    location.reload();

                },
                error: function (error) {
                    console.log(error);
                }
            });
        });
    });


}

 
function hideEmployeeDetailCard() {
    $("#EmployeeCard").hide();
}

function showEmployeeDetailCard() {
    $("#EmployeeCard").show();
}