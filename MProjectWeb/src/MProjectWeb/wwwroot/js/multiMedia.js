//mostrar texto mouse hover img
$(document).ready(function () {
    $("[rel='tooltip']").tooltip();

    $('.thumbnail').hover(
        function () {
            $(this).find('.caption').slideDown(250); //.fadeIn(250)
        },
        function () {
            $(this).find('.caption').slideUp(250); //.fadeOut(205)
        }
    );

    $('.gifVid').hover(
        function () {
            var src =  $(this).attr('src');
            $(this).attr('src', src.replace('.gif', '_anim.gif'));
        }, function () {
            var src = $(this).attr('src');
            $(this).attr('src', src);
        }
    );
    
});