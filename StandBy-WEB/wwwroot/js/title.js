jQuery.extend(jQuery.fn.dataTableExt.oSort, {
  "custom-sort-asc": function (a, b) {
    console.log("asc");
    if (typeof a === "string") {
      a = a.replace(/[^\d\.]/g, "");
    }
    if (typeof b === "string") {
      b = b.replace(/[^\d\.]/g, "");
    }
    a = parseInt(a.replace(" Dias", ""));
    b = parseInt(b.replace(" Dias", ""));
    if (a < 0 && b >= 0) {
      return -1;
    } else if (a >= 0 && b < 0) {
      return 1;
    } else {
      return a < b ? -1 : a > b ? 1 : 0;
    }
  },
  "custom-sort-desc": function (a, b) {
    console.log("desc");
    if (typeof a === "string") {
      a = a.replace(/[^\d\.]/g, "");
    }
    if (typeof b === "string") {
      b = b.replace(/[^\d\.]/g, "");
    }
    a = parseInt(a.replace(" Dias", ""));
    b = parseInt(b.replace(" Dias", ""));
    if (a < 0 && b >= 0) {
      return 1;
    } else if (a >= 0 && b < 0) {
      return -1;
    } else {
      return a < b ? 1 : a > b ? -1 : 0;
    }
  },
});
