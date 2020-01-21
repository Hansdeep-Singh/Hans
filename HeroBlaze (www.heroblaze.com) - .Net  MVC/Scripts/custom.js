$(document).ready(function () {
   

    var i = 0;
    $('.a',this).mousemove(function () {
        i += 1;
        if (i > 4) {
            i = 0;
        }
        else {
            $('.base',this).attr("src", "/Content/images/a/" + i + ".png");
        }
    });


    $('.b', this).mousemove(function () {
        i += 1;
        if (i > 4) {
            i = 0;
        }
        else {
            $('.base', this).attr("src", "/Content/images/b/" + i + ".png");
        }
    });

    $('.c', this).mousemove(function () {
        i += 1;
        if (i > 4) {
            i = 0;
        }
        else {
            $('.base', this).attr("src", "/Content/images/c/" + i + ".png");
        }
    });

    $('.d', this).mousemove(function () {
        i += 1;
        if (i > 4) {
            i = 0;
        }
        else {
            $('.base', this).attr("src", "/Content/images/d/" + i + ".png");
        }
    });

    //$('.join').hover(function () {
    //    $(this).animate({
    //        backgroundColor: "black",
    //        border: "black 2px",
    //        color: "fff"
    //    }, 2000);
    //});



    $(".join").hover(function () {
        $(this).addClass('anime_a');
        }
        , function () {

            $(this).removeClass('anime_a');
        
        });



    $(".buton").hover(function () {
        $(this).addClass('anime_b');
    }
        , function () {

            $(this).removeClass('anime_b');

        });


    $('#email').keydown(function () {
        $('.infoe').slideDown();
    });

   
    $('#email').focusout(function () {
        $('.infoe').slideUp();
    });

    $('#pass').keydown(function () {
        $('.infoa').slideDown();
    });

    $('#pass').focusout(function () {
        $('.infoa').slideUp();
    });

    $('#about').keydown(function () {
        $('.tab').slideDown();
    });

    $('#about').focusout(function () {
        $('.tab').slideUp();
    });

    $('#firstname').keydown(function () {
        $('.tfn').slideDown();
    });

    $('#firstname').focusout(function () {
        $('.tfn').slideUp();
    });

    $('#lastname').keydown(function () {
        $('.tln').slideDown();
    });

    $('#lastname').focusout(function () {
        $('.tln').slideUp();
    });

    $('#year').focusin(function () {
        $('.tyr').slideDown();
    });

    $('#year').focusout(function () {
        $('.tyr').slideUp();
    });

    $('#month').focusin(function () {
        $('.tmo').slideDown();
    });

    $('#month').focusout(function () {
        $('.tmo').slideUp();
    });

    $('#day').focusin(function () {
        $('.tdy').slideDown();
    });

    $('#day').focusout(function () {
        $('.tdy').slideUp();
    });

    $('.edit_pen').click(function () {
        $('.changephoto').stop().animate({ width: 'toggle' }, 350);
    });

    $('.rar').hover(function () {
        $('.ai').stop().animate({ width: 'toggle' }, 350);
    });

    $('.rb').hover(function () {
        $('.bi').stop().animate({ width: 'toggle' }, 350);
    });

    $('.rc').hover(function () {
        $('.ci').stop().animate({ width: 'toggle' }, 350);
    });

    $('.rd').hover(function () {
        $('.di').stop().animate({ width: 'toggle' }, 350);
    });

    $('.re').hover(function () {
        $('.ei').stop().animate({ width: 'toggle' }, 350);
    });

    $('.rf').hover(function () {
        $('.fi').stop().animate({ width: 'toggle' }, 350);
    });

    $('.rg').hover(function () {
        $('.gi').stop().animate({ width: 'toggle' }, 350);
    });

    $('.rh').hover(function () {
        $('.hi').stop().animate({ width: 'toggle' }, 350);
    });

    $('.add_talent_button').hover(function () {
        $('.add_talent_info').stop().animate({ width: 'toggle' }, 350);
    });

    $('.add_button').click(function () {
        $('.add_talent_form').stop().animate({ width: 'toggle' }, 350);
    });

    $('.ua').click(function () {
        $('.ca').stop().slideToggle(1400);
    });

    $('.ub').click(function () {
        $('.cb').stop().slideToggle(1400);
    });

    $('.uc').click(function () {
        $('.cc').stop().slideToggle(1400);
    });

    $('.ud').click(function () {
        $('.cd').stop().slideToggle(1400);
    });

    $('.documenttitle').hover(function () {
        $('.play_vid', this).stop().toggle("explode");
    });


    var playersrc = $('iframe').attr('src');

    $('documenttitle').hover(function () {
        $('.play_vid',this).find('iframe').attr('src', playersrc + '&autoplay=1');
    }, function () {
        $('.play_vid',this).find('iframe').attr('src', playersrc);
    });


    //$(".img_box").mouseenter(function () {
    //    $(".msg_box",this).stop().slideToggle(200);
    //});

    //$(".img_box").mouseleave(function () {
    //    $(".msg_box", this).stop().toggle("explode");
    //});

    $(".img_box").hover(function () {
        $(".msg_box", this).stop().toggle("explode");
    });

 

});








