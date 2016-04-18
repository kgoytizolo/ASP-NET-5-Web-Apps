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
    $("#sidebarToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $(this).text("Show Sidebar");
        }
        else {
            $(this).text("Hide Sidebar");
        }
    });
    
})();


