$(document).ready(function () {
    $(document).on("click", ".deleteContact", function () {
        $(this).closest("tr").remove();  
    });
    $("#addNew").click(function (e) {
        e.preventDefault(); 
        var $tableBody = $("#dataTable");
        var $trLast = $tableBody.find("tr:last");
        var $trNew = $trLast.clone();
        $trNew.find('input').val('');
        $trNew.find('input').prop('checked', false);
        $trNew.find('field-validation-error error').remove();
        var suffix = $trNew.find(':input:first').attr('name').match(/\d+/);          
               
        $trNew.find("td:last").html(' <button type="button"  name="btnDelete" id="btnDelete" class="deleteContact btn btn btn-danger btn-xs">Remove</button>');
        $.each($trNew.find(':input'), function (i, val) {
            var oldN = $(this).attr('name');
            var newN = oldN.replace('[' + suffix + ']', '[' + (parseInt(suffix) + 1) + ']');
            $(this).attr('name', newN);
            var type = $(this).attr('type');
            if (type != undefined) {
                if (type.toLowerCase() == "text") {
                    $(this).attr('value', '');
                }
            }
            $(this).removeClass("input-validation-error");
        });
        $trLast.after($trNew);
                
        var form = $("form")
            .removeData("validator")
            .removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(form);
    });

    $('a.remove').on("click", function (e) {


    });          
});
$(function () {
    jQuery.event.handle = jQuery.event.dispatch
});

function FetchBenefitPerct(ctr)
{
    var BType = 0;
    var BEmergency = false;
    var rowindex = ctr.parentNode.parentNode.rowIndex - 1;
    var ctrlName = ctr.name;
    if (ctrlName == "[" + rowindex + "].BenefitEmergency")
    {
        BEmergency = ctr.checked;
        BType = $('#dataTable tbody tr:eq(' + rowindex + ')').children('td').eq(5).find('option:selected').val();
    }
    else if (ctrlName == "[" + rowindex + "].BenefitId")
    {
        BType = ctr.selectedIndex;
        BEmergency = $('#dataTable tbody tr:eq(' + rowindex + ')').children('td').eq(4).find('input').is(':checked');
    }
    var claimamount = $('#dataTable tbody tr:eq(' + rowindex + ')').children('td').eq(3).find('input').val();
             
    var Benifitamount = 0;
    if (BType > 0) { 
        $.ajax({
            type: "POST",
            url: "/Claims/GetBenefitPerct/",
            data: {
                BenefitTyp: BType,
                EmergencyBenefit: BEmergency
            },
            success: function (result) {                   
                Benifitamount = result.BPT;                    
                $('#dataTable tbody tr:eq(' + rowindex + ')').children('td').eq(6).find('input').val(Benifitamount);
                CalculateApprvdAmt(ctr);
            }
        });
    }
}
function CalculateApprvdAmt(ctr) {
    var rowindex = ctr.parentNode.parentNode.rowIndex - 1;
    var claimAmount = $('#dataTable tbody tr:eq(' + rowindex + ')').children('td').eq(3).find('input').val();
    var benefirPerct = $('#dataTable tbody tr:eq(' + rowindex + ')').children('td').eq(6).find('input').val();
    var calcPrice = (claimAmount * benefirPerct / 100).toFixed(2);
    $('#dataTable tbody tr:eq(' + rowindex + ')').children('td').eq(7).find('input').val(calcPrice);
    calculateTotalApprovedAmt();
}
function calculateTotalApprovedAmt()
{            
    var rowCount = $('#dataTable tbody tr').length;
    var approvedamount = 0;           
    for (var i = 0; i < rowCount; i++) {
        var rowamount = $('#dataTable tbody tr:eq(' + i + ')').children('td').eq(7).find('input').val();
        approvedamount = (parseFloat(approvedamount) + parseFloat(rowamount));
    }
            
    $('#ApprovedTotalAmount').val(approvedamount.toFixed(2));

}
$(function () {
    $('.claim-details').on('click', function (e) {
        $.get($(this).prop('href'), function (response) {
            $('#divClaimDetails').html(response)
        });
        e.preventDefault();
    })
});
