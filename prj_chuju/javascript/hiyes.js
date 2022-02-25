// 地區 region, Rg
const theRegion = ['臺北市', '新北市', '桃園市', '新竹市', '新竹縣', '臺中市', '臺南市', '高雄市', '基隆市', '嘉義市', '彰化縣'];
// 坪數 area, Ar
//const theArea = ['不限', '15坪以下', '15-20坪', '20-30坪', '30-40坪', '40坪以上', '自訂'];
const theArea = ['不限', '15坪以下', '15-20坪', '20-30坪', '30-40坪', '40坪以上'];
const theAreaUB = [999, 15, 20, 30, 40, 999];
const theAreaLB = [0, 0, 15, 20, 30, 40];
// 房型 rooms, Rm
const theRooms = ['不限', '套房', '2房', '3房', '4房以上', '透天'];
const theRoomsCode = [0, 1, 2, 3, 4, 9];
// 屋況 status, St
const theStatus = ['不限', '預售屋', '新成屋'];
// 特性 tags, Tg
const theTags = ['不限', '學區宅', '交通宅', '景觀宅', '制震宅', '防疫宅', '低總價', '低公設', '低首付', '重劃區'];

function initSelector(selector) {
    initRegion(selector);
    initArea(selector);
    initRooms(selector);
    initStatus(selector);
    initTags(selector);
}

function initRegion(selector) {
    let innerHTML = "";
    $.each(theRegion, function (ind, val) {
        if (selector.region == ind) {
            innerHTML += `<input class="actived" id="SRg-${ind}" type="button" value="${val}">`;
        } else {
            innerHTML += `<input id="SRg-${ind}" type="button" value="${val}">`;
        }
    });
    let theHTML = `
        <fieldset>
            ${innerHTML}
        </fieldset>
        `;
    $('#regionRegion').html(theHTML);
    initRegionClick(selector);
}
function initArea(selector) {
    let innerHTML = "";
    $.each(theArea, function (ind, val) {
        if (selector.area == ind) {
            innerHTML += `<input class="actived" id="SAr-${ind}" type="button" value="${val}">`;
        } else {
            innerHTML += `<input id="SAr-${ind}" type="button" value="${val}">`;
        }
    });
    let theHTML = `
        <fieldset>
            ${innerHTML}
        </fieldset>
        `;
    $('#areaRegion').html(theHTML);
    initAreaClick(selector);
}
function initRooms(selector) {
    let isEmpty = selector.rooms.length == 0 || selector.rooms.indexOf(0) != -1;
    let theHTML = "";
    $.each(theRooms, function (ind, val) {
        theHTML += `
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="checkbox" id="SRm-${ind}" ${isEmpty ? ind == 0 ? "checked" : "" : selector.rooms.indexOf(ind) != -1 ? "checked" : ""}>
                <label class="form-check-label" for="SRm-${ind}">${val}</label>
            </div>
            `;
    });
    $('#roomsRegion').html(theHTML);
    initRoomsClick(selector);
}
function initStatus(selector) {
    let isEmpty = selector.status.length == 0 || selector.status.indexOf(0) != -1;
    let theHTML = "";
    $.each(theStatus, function (ind, val) {
        theHTML += `
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="checkbox" id="SSt-${ind}" ${isEmpty ? ind == 0 ? "checked" : "" : selector.status.indexOf(ind) != -1 ? "checked" : ""}>
                <label class="form-check-label" for="SSt-${ind}">${val}</label>
            </div>
            `;
    });
    $('#statusRegion').html(theHTML);
    initStatusClick(selector);
}
function initTags(selector) {
    let isEmpty = selector.tags.length == 0 || selector.tags.indexOf(0) != -1;
    let theHTML = "";
    $.each(theTags, function (ind, val) {
        theHTML += `
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="checkbox" id="STg-${ind}" ${isEmpty ? ind == 0 ? "checked" : "" : selector.tags.indexOf(ind) != -1 ? "checked" : ""}>
                <label class="form-check-label" for="STg-${ind}">${val}</label>
            </div>
            `;
    });
    $('#tagsRegion').html(theHTML);
    initTagsClick(selector);
}

function initRegionClick(selector) {
    $.each(theRegion, function (ind, val) {
        $(`#SRg-${ind}`).on('click', function () {
            $.each(theRegion, function (i, v) {
                $(`#SRg-${i}`).removeClass('actived');
            })
            if (selector.region == ind) {
                selector.region = -1;
            } else {
                $(this).addClass('actived');
                selector.region = ind;
            }
            selector.update();
        });
    });
}
function initAreaClick(selector) {
    $.each(theArea, function (ind, val) {
        $(`#SAr-${ind}`).on('click', function () {
            $.each(theArea, function (i, v) {
                $(`#SAr-${i}`).removeClass('actived');
            })
            if (selector.area == ind) {
                selector.area = -1;
            } else {
                $(this).addClass('actived');
                selector.area = ind;
            }
            selector.update();
        });
    });
}
function initRoomsClick(selector) {
    $(`#SRm-0`).change(function () {
        if ($(this)[0].checked == true) {
            selector.rooms = [0];
            for (let i = 1; i < theRooms.length; i++) {
                $(`#SRm-${i}`)[0].checked = false;
            }
        } else {
            removeXinArray(0, selector.rooms);
        }
        selector.update();
    })
    for (let i = 1; i < theRooms.length; i++) {
        $(`#SRm-${i}`).change(function () {
            if ($(this)[0].checked == true) {
                $('#SRm-0')[0].checked = false;
                removeXinArray(0, selector.rooms);
                selector.rooms.push(i);
            } else {
                removeXinArray(i, selector.rooms);
            }
            selector.update();
        })
    }
}
function initStatusClick(selector) {
    $(`#SSt-0`).change(function () {
        if ($(this)[0].checked == true) {
            selector.status = [0];
            for (let i = 1; i < theStatus.length; i++) {
                $(`#SSt-${i}`)[0].checked = false;
            }
        } else {
            removeXinArray(0, selector.status);
        }
        selector.update();
    })
    for (let i = 1; i < theStatus.length; i++) {
        $(`#SSt-${i}`).change(function () {
            if ($(this)[0].checked == true) {
                $('#SSt-0')[0].checked = false;
                removeXinArray(0, selector.status);
                selector.status.push(i);
            } else {
                removeXinArray(i, selector.status);
            }
            selector.update();
        })
    }
}
function initTagsClick(selector) {
    $(`#STg-0`).change(function () {
        if ($(this)[0].checked == true) {
            selector.tags = [0];
            for (let i = 1; i < theTags.length; i++) {
                $(`#STg-${i}`)[0].checked = false;
            }
        } else {
            removeXinArray(0, selector.tags);
        }
        selector.update();
    })
    for (let i = 1; i < theTags.length; i++) {
        $(`#STg-${i}`).change(function () {
            if ($(this)[0].checked == true) {
                $('#STg-0')[0].checked = false;
                removeXinArray(0, selector.tags);
                selector.tags.push(i);
            } else {
                removeXinArray(i, selector.tags);
            }
            selector.update();
        })
    }
}

function removeXinArray(x, array) {
    let i = 0;
    while (i < array.length) {
        if (array[i] == x) {
            array.splice(i, 1);
        } else {
            i++;
        }
    }
}

const nullSelectorObj = {
    region: -1,
    area: -1,
    rooms: [0],
    status: [0],
    tags: [0],
}
function selectorObj(initObj) {
    let self = this;
    this.region = initObj.region;
    this.area = initObj.area;
    this.rooms = initObj.rooms;
    this.status = initObj.status;
    this.tags = initObj.tags;
    this.ajaxData = {};
    this.update = function () {
        self.generateAjaxData();
    };
    this.generateAjaxData = function () {
        let roomsStr = "0";
        let statusStr = "0";
        let tagsStr = "0";
        for (let i = 0; i < self.rooms.length; i++) {
            let n = self.rooms[i];
            if (n == 0) {
                roomsStr = "0";
                break;
            }
            roomsStr += n;
        }
        for (let i = 0; i < self.status.length; i++) {
            let n = self.status[i];
            if (n == 0) {
                statusStr = "0";
                break;
            }
            statusStr += n;
        }
        for (let i = 0; i < self.tags.length; i++) {
            let n = self.tags[i];
            if (n == 0) {
                tagsStr = "0";
                break;
            }
            tagsStr += n;
        }
        self.ajaxData = {
            region: self.region,
            area: self.area,
            rooms: roomsStr,
            status: statusStr,
            tags: tagsStr,
        };
    };

}

let selector = new selectorObj(nullSelectorObj);

$(function () {
    initSelector(selector);

    $('#submitSearchBtn').on('click', function () {
        $('#toSearchRegion').val(selector.ajaxData.region);
        $('#toSearchArea').val(selector.ajaxData.area);
        $('#toSearchRooms').val(selector.ajaxData.rooms);
        $('#toSearchStatus').val(selector.ajaxData.status);
        $('#toSearchTags').val(selector.ajaxData.tags);
        $('#buildingSearchForm').submit();
    })
})