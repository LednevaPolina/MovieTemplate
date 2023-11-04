
//$('img').on ('error', function () {
//    $(this).attr('scr', '/images/No_Image.png');
//})

$('[data-open-modal]').click(async function () {
    event.preventDefault();

    let url = $(this).attr('href');
    let response = await fetch(url);
    let result = await response.text();

    $('.modal-body').html(result);
    $('#exampleModal').modal('show');
});


let page;
let totalPages;
let url;

//function initPagination(p, t, u) {
//    page = p;
//    totalPages = t;
//    url = u;
//}

//$('#buttonNext').click(async function () {
//    page++;
//    let response = await fetch(`${url}&page=${page}`);
//    let result = await response.text();

//    $('.row').append(result);

//    if (page == totalPages) {
//        $(this).remove()
//    }
//});

let isScroll = true;

$(window).scroll(async function () {
    if ($(window).scrollTop() + $(window).height() > $(document).height() - 5000 && isScroll)
    {
        isScroll = false;
        if (page < totalPages)
        {
            page++;
            let response = await fetch(`${url}&page=${page}`);
            let result = await response.text();

            $('.myList').append(result);
        }
        if (page >= totalPages) {
            $('#buttonNext').remove()
        }

        isScroll = true;
    }

});

