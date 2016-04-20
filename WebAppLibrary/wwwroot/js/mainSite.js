//Code is executed for exclusive use
//Function outside global scope
(function () {
    //var main = $("#main");                                   //JQuery replaces document.getElementById
    //main.on("mouseenter", function () {                     //On indicates Events
    //    main.style = "background-color: #FFF;";
    //});
    //main.on("mouseleave", function () {                     //On indicates events
    //    main.style = "background-color: antiquewhite;";
    //});

    //var menuItems = $("ul.menu li a");                      //Will applicate to ul.menu li a, can manage events in one callback
    //menuItems.on("click", function () {
    //    var me = $(this);
    //    alert(me.text());                                   //Gets de text of each clicked menu
    //});

    var $sidebarAndWrapper = $("#sidebar,#wrapper");        //Also define variables with $ to diferentiate from clasic javascript
    var $icon = $("#sidebarToggle i.fa");                   //Look for the italic efect and favicon font to apply the effect

    $("#sidebarToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        }
        else {
            $icon.addClass("fa-angle-left");
            $icon.removeClass("fa-angle-right");
        }
    });
    
})();


