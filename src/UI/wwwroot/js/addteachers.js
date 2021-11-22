var nauczycieleId = 0;
let nauczycieleList = [];
class Nauczyciel {
    constructor(imie, nazwisko, ilosc_godzin, tablica) {
        this.id = nauczycieleId++;
        this.imie = imie;
        this.nazwisko = nazwisko;
        this.ilosc_godzin = ilosc_godzin;
        this.dostepnoscArr = tablica;
    }

}


class UI {

    static addNauczycielToList(nauczyciel) {

        const list2 = document.querySelector('.all-seperate');
        const row2 = document.createElement('button');
        row2.className = "unhiden_item dark-shadow";

        row2.innerHTML = `

                <ul class="item">
                            <li> <p>${nauczyciel.imie}</p> </li>
                            <li> <p>${nauczyciel.nazwisko}</p> </li>
                                <li><p>${nauczyciel.ilosc_godzin}</p> </li>
                           
                            <li>
                                <div class="btn-container">
                                    <input class="btn btn-dark" type="submit" value="USUŃ">
                                </div>
                            </li>
                 </ul>
                `;


        row2.addEventListener("click", function () {
            this.classList.toggle("active");
            var panel = this.nextElementSibling;
            if (panel.style.maxHeight) {
                panel.style.maxHeight = null;
            } else {
                panel.style.maxHeight = panel.scrollHeight + "px";
            }
        });
        list2.appendChild(row2);

        const row3 = document.createElement('div');
        row3.className = 'panel';

        const row4 = document.createElement('table');
        row4.className = 'hidden_item';
        row4.innerHTML = `
                <thead>
                            <tr>
                                <tr>
                                    <th>Godziny</th>
                                    <th>Poniedziałek</th>
                                    <th>Wtorek</th>
                                    <th>Środa</th>
                                    <th>Czwartek</th>
                                    <th>Piątek</th>
                                   </tr>
                            </tr>
                </thead>
                `;

        const row6 = document.createElement('tbody');
        for (var i = 0; i < nauczyciel.dostepnoscArr.length; i++) {
            const row5 = document.createElement('tr');

            row5.innerHTML = `
                    <td>${i + 8}-${i + 9}</td>
                    `;

            for (var j = 0; j < nauczyciel.dostepnoscArr[i].length; j++) {
                const row7 = document.createElement('td');
                if (nauczyciel.dostepnoscArr[i][j] == 1) {
                    row7.innerHTML = `<td><input type="checkbox" ></td>`;
                }
                else {
                    row7.innerHTML = `<td><input type="checkbox" checked ></td>`;
                }

                row5.appendChild(row7);
            }

            row6.appendChild(row5);
        }


        row4.appendChild(row6);
        row3.appendChild(row4);
        list2.appendChild(row3);

    }
    static clearFields() {
        document.querySelector('#imie').value = '';
        document.querySelector('#nazwisko').value = '';
        document.querySelector('#count').value = '';
        var grid = document.getElementById("add-table");
        var checkBoxes = grid.getElementsByTagName("INPUT");

        for (var i = 0; i < checkBoxes.length; i++) {
            checkBoxes[i].checked = false;
        }

    }
}

var addNew = document.querySelector('#addNew');
addNew.addEventListener('click', (e) => {
    e.preventDefault();

    const imie = document.querySelector('#imie').value;
    const nazwisko = document.querySelector('#nazwisko').value;
    const ilosc = document.querySelector('#count').value;



    let dostepnosc = [];
    let array8 = [], array9 = [], array10 = [], array11 = [], array12 = [], array13 = [], array14 = [], array15 = [], array16 = [], array17 = [];
    var grid = document.getElementById("8");
    var checkBoxes = grid.getElementsByTagName("INPUT");

    for (var i = 0; i < checkBoxes.length; i++) {
        if (checkBoxes[i].checked)
            array8[i] = "0";
        else array8[i] = "1";
    }
    dostepnosc.push(array8);

    grid = document.getElementById("9");
    checkBoxes = grid.getElementsByTagName("INPUT");

    for (var i = 0; i < checkBoxes.length; i++) {
        if (checkBoxes[i].checked)
            array9[i] = "0";
        else array9[i] = "1";
    }
    dostepnosc.push(array9);

    grid = document.getElementById("10");
    checkBoxes = grid.getElementsByTagName("INPUT");

    for (var i = 0; i < checkBoxes.length; i++) {
        if (checkBoxes[i].checked)
            array10[i] = "0";
        else array10[i] = "1";
    }
    dostepnosc.push(array10);

    grid = document.getElementById("11");
    checkBoxes = grid.getElementsByTagName("INPUT");

    for (var i = 0; i < checkBoxes.length; i++) {
        if (checkBoxes[i].checked)
            array11[i] = "0";
        else array11[i] = "1";
    }
    dostepnosc.push(array11);

    grid = document.getElementById("12");
    checkBoxes = grid.getElementsByTagName("INPUT");
    for (var i = 0; i < checkBoxes.length; i++) {
        if (checkBoxes[i].checked)
            array12[i] = "0";
        else array12[i] = "1";
    }
    dostepnosc.push(array12);

    grid = document.getElementById("13");
    checkBoxes = grid.getElementsByTagName("INPUT");
    for (var i = 0; i < checkBoxes.length; i++) {
        if (checkBoxes[i].checked)
            array13[i] = "0";
        else array13[i] = "1";
    }
    dostepnosc.push(array13);

    grid = document.getElementById("14");
    checkBoxes = grid.getElementsByTagName("INPUT");
    for (var i = 0; i < checkBoxes.length; i++) {
        if (checkBoxes[i].checked)
            array14[i] = "0";
        else array14[i] = "1";
    }
    dostepnosc.push(array14);

    grid = document.getElementById("15");
    checkBoxes = grid.getElementsByTagName("INPUT");
    for (var i = 0; i < checkBoxes.length; i++) {
        if (checkBoxes[i].checked)
            array15[i] = "0";
        else array15[i] = "1";
    }
    dostepnosc.push(array15);

    grid = document.getElementById("16");
    checkBoxes = grid.getElementsByTagName("INPUT");
    for (var i = 0; i < checkBoxes.length; i++) {
        if (checkBoxes[i].checked)
            array16[i] = "0";
        else array16[i] = "1";
    }
    dostepnosc.push(array16);

    console.log(dostepnosc);

    var nauczyciel = new Nauczyciel(imie, nazwisko, ilosc, dostepnosc);

    nauczycieleList.push(nauczyciel);

    UI.addNauczycielToList(nauczyciel);
    localStorage.setItem('MyClasses', JSON.stringify(nauczycieleList));
    UI.clearFields();
    hidden();
});