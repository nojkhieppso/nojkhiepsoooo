    function delete_row(row){
        var box = $("#mb-remove-row");
        box.addClass("open");
        box.find(".mb-control-yes").on("click",function(){
            box.removeClass("open");
            $("#" + Delete).hide("slow", function () {
                $(this).remove();
            });
        });
    }
