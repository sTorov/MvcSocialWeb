const ddlMonth = document.getElementById("ddlMonth");
const ddlYear = document.getElementById("ddlYear");
const ddlDay = document.getElementById("ddlDay");

function PopulateDays() {
    const ddlDay = document.getElementById("ddlDay");

    const y = ddlYear.options[ddlYear.selectedIndex].value;
    const m = ddlMonth.options[ddlMonth.selectedIndex].value;

    if (m != 0 && y != 0) {
        const dayCount = 32 - new Date(y, m - 1, 32).getDate();

        if(ddlDay.options[0].value == 0)
            ddlDay.options.length = 1;
        else 
            ddlDay.options.length = 0;

        for (let i = 1; i <= dayCount; i++) {
            AddOption(ddlDay, i, i);
        }

        ddlDay.classList.remove('input-validation-error');
    }
}

function AddOption(ddl, text, value) {
    const opt = document.createElement("option");
    opt.text = text;
    opt.value = value;
    ddl.options.add(opt);
}

function RemoveOption(index) {
    if(index > 0)
        ddlDay.options.remove(index);

    ddlMonth.options.remove(index);
    ddlYear.options.remove(index);
}

function FirstSelectOption(select, callback) {
    select.style.color = '#212529';

    if (select == ddlDay) {
        for (let i = 0; i < select.options.length; i++) {
            if (select.options[i].value == 1) {
                select.options.selectedIndex = i;
                break;
            }
        }
    }

    RemoveEventListener(select, callback);
}

function RemoveEventListener(select, callback) {
    select.removeEventListener("change", callback);
    select.removeEventListener("DOMNodeInserted", callback)
}

function AddDefaultOption(){
    const values = ['Day', 'Month', 'Year'];
    const selected = [ddlDay, ddlMonth, ddlYear];

    for (let i = 0; i < 3; i++) {
        let opt = document.createElement('option');
        opt.setAttribute('selected', 'selected');
        opt.setAttribute('disabled', 'disabled');
        opt.classList.add("default-opt");
        opt.innerText = values[i];
        opt.setAttribute('value', 0);
        selected[i].insertAdjacentElement('afterbegin', opt);
    }
}

const ddlDaysFirstSelect = () => FirstSelectOption(ddlDay, ddlDaysFirstSelect);
const ddlMonthFirstSelect = () => FirstSelectOption(ddlMonth, ddlMonthFirstSelect);
const ddlYearFirstSelect = () => FirstSelectOption(ddlYear, ddlYearFirstSelect);

ddlMonth.addEventListener("change", PopulateDays);
ddlYear.addEventListener("change", PopulateDays);

function AddChangeStyleEvent() {
    ddlDay.addEventListener("DOMNodeInserted", ddlDaysFirstSelect);
    ddlMonth.addEventListener("change", ddlMonthFirstSelect);
    ddlYear.addEventListener("change", ddlYearFirstSelect);
}

function OnChatLoad() {
    let chatBlock = document.querySelector('.chat-block');
    let textarea = document.querySelector('.textarea');
    let btn = document.querySelector('.btn_submit');


    window.onscroll = () => {
        localStorage['scrollPos'] = window.scrollY;
    };
    window.onload = () => {
        if (localStorage['scrollPos'] != null)
            window.scrollTo({ left: 0, top: localStorage['scrollPos'], behavior: "instant" });
        if (sessionStorage['message'] != null) {
            textarea.value = sessionStorage['message'];

            if (sessionStorage['message'] != '')
                textarea.focus();
        }

        if (sessionStorage['chatScroll'] != '')
            chatBlock.scrollTop = sessionStorage['chatScroll'];
        else
            chatBlock.scrollTop = chatBlock.scrollHeight;
    };
    textarea.oninput = () => {
        sessionStorage['message'] = textarea.value;
    };
    btn.onclick = () => {
        sessionStorage['message'] = '';
        sessionStorage['chatScroll'] = '';
    };
    chatBlock.onscroll = () => {
        let checkValue = window.screen.width < 992 ?
            325 : 345;

        let dev = chatBlock.scrollHeight - chatBlock.scrollTop;

        sessionStorage['chatScroll'] = dev < checkValue
            ? ''
            : chatBlock.scrollTop;
    };
}