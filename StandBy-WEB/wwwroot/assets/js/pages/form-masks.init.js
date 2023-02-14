if (document.querySelector("#cleave-valor-cartao")) {
  var cleavePrefix = new Cleave("#cleave-valor-cartao", {
    delimiters: ["R$", ","],
    blocks: [0, 3, 2],
    numericOnly: true,
  });
}

if (document.querySelector("#cleave-valor-avista")) {
  var cleaveDate = new Cleave("#cleave-valor-avista", {
    delimiters: ["R$", ","],
    blocks: [0, 3, 2],
    numericOnly: true,
  });
}

if (document.querySelector("#cleave-valor-parcelas")) {
  var cleaveDate = new Cleave("#cleave-valor-parcelas", {
    delimiter: "",
    blocks: [2],
    numericOnly: true,
  });
}

if (document.querySelector("#cleave-valor-vparcela")) {
  var cleavePrefix = new Cleave("#cleave-valor-vparcela", {
    delimiters: ["R$", ","],
    blocks: [0, 3, 2],
    numericOnly: true,
  });
}

if (document.querySelector("#cleave-cep")) {
  var cleaveDate = new Cleave("#cleave-cep", {
    delimiters: ["-"],
    blocks: [5, 3],
  });
}

if (document.querySelector("#cleave-date")) {
  var cleaveDate = new Cleave("#cleave-date", {
    date: true,
    delimiter: "-",
    datePattern: ["d", "m", "Y"],
  });
}

if (document.querySelector("#cleave-date-servicos")) {
  var cleaveDate = new Cleave("#cleave-date-servicos", {
    date: true,
    delimiter: "-",
    datePattern: ["d", "m", "Y"],
  });
}

if (document.querySelector("#cleave-date1")) {
  var cleaveDate = new Cleave("#cleave-date1", {
    date: true,
    delimiter: "-",
    datePattern: ["d", "m", "Y"],
  });
}

if (document.querySelector("#cleave-telefone")) {
  var cleaveTelefone = new Cleave("#cleave-telefone", {
    delimiters: ["(", ") ", " ", "-"],
    blocks: [0, 2, 5, 4],
  });

  document
    .querySelector("#cleave-telefone")
    .addEventListener("input", function (data) {
      if (data.inputType == "deleteContentBackward") {
        cleaveTelefone.destroy();
      } else {
        cleaveTelefone.destroy();
        cleaveTelefone = new Cleave("#cleave-telefone", {
          delimiters: ["(", ") ", " ", "-"],
          blocks: [0, 2, 5, 4],
        });
      }
    });
}

if (document.querySelector("#cleave-telefone-recado")) {
  var cleaveTelefoneRecado = new Cleave("#cleave-telefone-recado", {
    delimiters: ["(", ") ", " ", "-"],
    blocks: [0, 2, 5, 4],
  });

  document
    .querySelector("#cleave-telefone-recado")
    .addEventListener("input", function (data) {
      if (data.inputType == "deleteContentBackward") {
        cleaveTelefoneRecado.destroy();
      } else {
        cleaveTelefoneRecado.destroy();
        cleaveTelefoneRecado = new Cleave("#cleave-telefone-recado", {
          delimiters: ["(", ") ", " ", "-"],
          blocks: [0, 2, 5, 4],
        });
      }
    });
}

if (document.querySelector("#cleave-cpf")) {
  var cleaveCPF = new Cleave("#cleave-cpf", {
    delimiters: [".", ".", "-"],
    blocks: [3, 3, 3, 2],
    numericOnly: true,
  });

  document
    .querySelector("#cleave-cpf")
    .addEventListener("input", function (data) {
      if (data.inputType == "deleteContentBackward") {
        cleaveCPF.destroy();
      } else {
        if (this.value.length > 14) {
          cleaveCPF.destroy();
          cleaveCPF = new Cleave("#cleave-cpf", {
            delimiters: [".", ".", "/", "-"],
            blocks: [2, 3, 3, 4, 2],
            numericOnly: true,
          });
        } else {
          cleaveCPF.destroy();
          cleaveCPF = new Cleave("#cleave-cpf", {
            delimiters: [".", ".", "-"],
            blocks: [3, 3, 3, 2],
            numericOnly: true,
          });
        }
      }
    });
}

//if (document.querySelector("#cleave-cpf")) {
//    document.querySelector("#cleave-cpf").addEventListener("input", function () {
//        if (this.value.length == 18) {
//            cleaveCPF.destroy();
//            cleaveCPF = new Cleave('#cleave-cpf', {
//                delimiters: ['.', '.', '/', '-'],
//                blocks: [2, 3, 3, 4, 2]
//            });
//        } else {
//            cleaveCPF.destroy();
//            cleaveCPF = new Cleave('#cleave-cpf', {
//                delimiters: ['.', '.', '-'],
//                blocks: [3, 3, 3, 2]
//            });
//        }
//    });
//}

//if (document.querySelector("#cleave-cnpj")) {
//    var cleaveDate = new Cleave('#cleave-cnpj', {
//        delimiters: ['.', '.', '/', '-'],
//        blocks: [2, 3, 3, 4, 2]
//    });
//}

if (document.querySelector("#cleave-date-format")) {
  var cleaveDateFormat = new Cleave("#cleave-date-format", {
    date: true,
    datePattern: ["m", "y"],
  });
}

if (document.querySelector("#cleave-time")) {
  var cleaveTime = new Cleave("#cleave-time", {
    time: true,
    timePattern: ["h", "m", "s"],
  });
}

if (document.querySelector("#cleave-time-format")) {
  var cleaveTimeFormat = new Cleave("#cleave-time-format", {
    time: true,
    timePattern: ["h", "m"],
  });
}

if (document.querySelector("#cleave-numeral")) {
  var cleaveNumeral = new Cleave("#cleave-numeral", {
    numeral: true,
    numeralThousandsGroupStyle: "thousand",
  });
}

if (document.querySelector("#cleave-ccard")) {
  var cleaveBlocks = new Cleave("#cleave-ccard", {
    blocks: [4, 4, 4, 4],
    uppercase: true,
  });
}

if (document.querySelector("#cleave-delimiter")) {
  var cleaveDelimiter = new Cleave("#cleave-delimiter", {
    delimiter: "·",
    blocks: [3, 3, 3],
    uppercase: true,
  });
}

if (document.querySelector("#cleave-delimiters")) {
  var cleaveDelimiters = new Cleave("#cleave-delimiters", {
    delimiters: [".", ".", "-"],
    blocks: [3, 3, 3, 2],
    uppercase: true,
  });
}

if (document.querySelector("#cleave-prefix")) {
  var cleavePrefix = new Cleave("#cleave-prefix", {
    prefix: "PREFIX",
    delimiter: "-",
    blocks: [6, 4, 4, 4],
    uppercase: true,
  });
}
