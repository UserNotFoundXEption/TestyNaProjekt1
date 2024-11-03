function isWindows() {
  isWindows = navigator.platform.indexOf('Win') > -1 ? true : false;

  if (isWindows) {
    // if we are on windows OS we activate the perfectScrollbar function
    var ps = new PerfectScrollbar('.sidebar-wrapper');
    var ps2 = new PerfectScrollbar('.main-panel');

    $('html').addClass('perfect-scrollbar-on');
  } else {
    $('html').addClass('perfect-scrollbar-off');
  }
};

transparent = true;
transparentDemo = true;
fixedTop = false;

navbar_initialized = false;
backgroundOrange = false;
sidebar_mini_active = false;
toggle_initialized = false;

var is_iPad = navigator.userAgent.match(/iPad/i) != null;
var scrollElement = navigator.platform.indexOf('Win') > -1 ? $(".main-panel") : $(window);

seq = 0, delays = 80, durations = 500;
seq2 = 0, delays2 = 80, durations2 = 500;

$(document).ready(function () {
    if ($('.full-screen-map').length == 0 && $('.bd-docs').length == 0) {
        $('.collapse').on('show.bs.collapse', function () {
            $(this).closest('.navbar').removeClass('navbar-transparent').addClass('bg-white');
        }).on('hide.bs.collapse', function () {
            $(this).closest('.navbar').addClass('navbar-transparent').removeClass('bg-white');
        });
    }

    $navbar = $('.navbar[color-on-scroll]');
    scroll_distance = $navbar.attr('color-on-scroll') || 500;

    if ($('.navbar[color-on-scroll]').length != 0) {
        nowuiDashboard.checkScrollForTransparentNavbar();
        $(window).on('scroll', nowuiDashboard.checkScrollForTransparentNavbar)
    }

    $('.form-control').on("focus", function () {
        $(this).parent('.input-group').addClass("input-group-focus");
    }).on("blur", function () {
        $(this).parent(".input-group").removeClass("input-group-focus");
    });

    $('.bootstrap-switch').each(function () {
        $this = $(this);
        data_on_label = $this.data('on-label') || '';
        data_off_label = $this.data('off-label') || '';

        $this.bootstrapSwitch({
            onText: data_on_label,
            offText: data_off_label
        });
    });

    document.getElementById('searchForm').addEventListener('submit', search);
});

$(document).on('click', '.navbar-toggle', function () {
    $toggle = $(this);

    if (nowuiDashboard.misc.navbar_menu_visible == 1) {
        $('html').removeClass('nav-open');
        nowuiDashboard.misc.navbar_menu_visible = 0;
        setTimeout(function () {
            $toggle.removeClass('toggled');
            $('#bodyClick').remove();
        }, 550);

    } else {
        setTimeout(function () {
            $toggle.addClass('toggled');
        }, 580);

        div = '<div id="bodyClick"></div>';
        $(div).appendTo('body').click(function () {
            $('html').removeClass('nav-open');
            nowuiDashboard.misc.navbar_menu_visible = 0;
            setTimeout(function () {
                $toggle.removeClass('toggled');
                $('#bodyClick').remove();
            }, 550);
        });

        $('html').addClass('nav-open');
        nowuiDashboard.misc.navbar_menu_visible = 1;
    }
});

$(window).resize(function () {
    seq = seq2 = 0;

    if ($('.full-screen-map').length == 0 && $('.bd-docs').length == 0) {

        $navbar = $('.navbar');
        isExpanded = $('.navbar').find('[data-toggle="collapse"]').attr("aria-expanded");
        if ($navbar.hasClass('bg-white') && $(window).width() > 991) {
            if (scrollElement.scrollTop() == 0) {
                $navbar.removeClass('bg-white').addClass('navbar-transparent');
            }
        } else if ($navbar.hasClass('navbar-transparent') && $(window).width() < 991 && isExpanded != "false") {
            $navbar.addClass('bg-white').removeClass('navbar-transparent');
        }
    }
    if (is_iPad) {
        $('body').removeClass('sidebar-mini');
    }
});

nowuiDashboard = {
    misc: {
        navbar_menu_visible: 0
    },

    showNotification: function (from, align) {
        color = 'primary';

        $.notify({
            icon: "now-ui-icons ui-1_bell-53",
            message: "Welcome to <b>Now Ui Dashboard</b> - a beautiful freebie for every web developer."

        }, {
            type: color,
            timer: 8000,
            placement: {
                from: from,
                align: align
            }
        });
    }


};

function hexToRGB(hex, alpha) {
    var r = parseInt(hex.slice(1, 3), 16),
        g = parseInt(hex.slice(3, 5), 16),
        b = parseInt(hex.slice(5, 7), 16);

    if (alpha) {
        return "rgba(" + r + ", " + g + ", " + b + ", " + alpha + ")";
    } else {
        return "rgb(" + r + ", " + g + ", " + b + ")";
    }
}

async function getBookList() {
    try {
        const response = await fetch('/api/Books?type=list', { method: 'GET' });
        if (!response.ok) {
            throw new Error('Failed to load books');
        }
        const books = await response.json();
        return books;
    } catch (error) {
        console.error('Error:', error);
        alert('Failed to load books');
        return [];
    }
}

async function updateBookData() {
    const books = await getBookList();
    updateBookTable(books);
}

async function tryToUpdateChart() {
    if (document.getElementById('bigDashboardChart') != undefined) {
        try {
            const response = await fetch('/api/Books?type=chart', { method: 'GET' });
            if (!response.ok) {
                throw new Error('Failed to load chart data');
            }
            const data = await response.json();
            if (data != undefined) {
                updateChart(data);
            }
        } catch (error) {
            alert('Failed to load chart data');
            return [];
        }
    }
}

async function deleteBook(bookId) {
    try {
        const response = await fetch(`/api/Books/${bookId}`, {
            method: 'DELETE'
        });
        const result = await response.json();
        if (response.ok) { 
            updateBookData();
            tryToUpdateChart();
            alert(result.message);
        } else {
            alert(result.message);
        }
    } catch (error) {
        console.error('Error:', error);
        alert('Failed to delete book');
    }
}

async function changeCount(command, bookId) {
    const response = await fetch('/api/CountChange', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(command+bookId)
    })
        .then(response => {
            if (response.ok) {
                if (window.location.pathname.includes('search')) {
                    updateSearchResults();
                }
                else {
                updateBookData();
                }
                const bookForm = document.getElementById('bookForm');
                if (bookForm != undefined) {
                    bookForm.reset()
                }
                tryToUpdateChart();
            } else {
                alert('Failed to change count');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Connection failure.');
        });
}



