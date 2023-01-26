const ddlMonth = document.getElementById("ddlMonth");
const ddlYear = document.getElementById("ddlYear");
const ddlDay = document.getElementById("ddlDay");

function PopulateDays() {
    const y = ddlYear.options[ddlYear.selectedIndex].value;
    const m = ddlMonth.options[ddlMonth.selectedIndex].value;

    if (m != 0 && y != 0) {
        const dayCount = 32 - new Date(y, m - 1, 32).getDate();
        ddlDay.options.length = 0;
        for (let i = 1; i <= dayCount; i++) {
            AddOption(ddlDay, i, i);
        }
    }
}

function AddOption(ddl, text, value) {
    const opt = document.createElement("option");
    opt.text = text;
    opt.value = value;
    ddl.options.add(opt);
}

function RemoveFirstOptionForYearAndMonth() {
    ddlMonth.options.remove(0);
    ddlYear.options.remove(0);
}