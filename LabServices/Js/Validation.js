alert();
$(document).ready(function () {
    alert();
    $("#Login").click(function () {
     
        var userid = $("#userId").val();
        var password = $("#Password").val();
      
        if (userid.length==0) {
            $("#UserLable").text("**");
            return false;
        }
        if (password.length == 0) {
            alert();
            $("#PasswordLabel").text("**");
       
            return false;
        }
        return true;
       
    });


});
