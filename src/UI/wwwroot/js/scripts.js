
localStorage.clear();

var studentId = 0;
var studentsList = [];
class Student {

    constructor(imie, nazwisko) {
        this.id = studentId++;
        this.imie = imie;
        this.nazwisko = nazwisko;
    }


    get Imie() {
        return this.imie;
    }
    get Nazwisko() {
        return this.nazwisko;
    }

}

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

var classID = 0;
let classesList = [];

class Class {
    constructor(name) {
        this.id = classID++;
        this.name = name;
        this.studentsArr = [];
    }
    newStudent(imie, nazwisko) {
        let s = new Student(imie, nazwisko);
        this.studentsArr.push(s);
        return s;
    }

    allStudents() {
        return this.studentsArr
    }

    get numberOfPlayers() {
        return this.studentsArr.length
    }

    ChangeName(name) {
        this.name = name;
    }

    Clear() {
        this.studentsArr = [];
    }

    Reset() {
        this.name = tmp;
        this.id = classID++;
        this.studentsArr = [];
    }

}

var classroomId = 0;
var clasroomsList = [];
class Classroom {

    constructor(kod, nazwa, ilosc_miejsc) {
        this.id = classroomId++;
        this.kod = kod;
        this.nazwa = nazwa;
        this.ilosc_miejsc = ilosc_miejsc;
    }


    get Kod() {
        return this.kod;
    }
    get Nazwa() {
        return this.nazwa;
    }
    get Ilosc_miejsc() {
        return this.ilosc_miejsc;
    }
}

class UI {
    static displayStudents() {
    }

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

    static displayClassroom() {
        var tmp = new Classroom("czyt", "czytelnia", "30");

        UI.addClassRoomToList(tmp);
    }

    static addClassRoomToList(klasa) {

        const list2 = document.querySelector('.all');
        const row2 = document.createElement('li');

        row2.innerHTML = `

            <ul class="item">
             <li> <p>${klasa.kod}</p></li>
             <li> <p>${klasa.nazwa}</p></li>
             <li> <p>${klasa.ilosc_miejsc}</p> </li>
             <li> <div class="btn-container">
              <input class="btn btn-dark" type="submit" value="USUŃ">
            
                   </div>
            </li>
            </ul>
        `;

        list2.appendChild(row2);
    }
    static clearFields() {
        document.querySelector('#kod').placeholder = 'kod';
        document.querySelector('#name').placeholder = 'nazwa';
        document.querySelector('#count').placeholder = '1';

    }

    static addStudentToList(student) {

        const list = document.querySelector('#studentList');

        const row = document.createElement('ul');


        row.innerHTML = `
    <li>${student.imie} ${student.nazwisko}</li>

    <li><div class="btn-container">
        <input class="btn btn-dark" type="submit" value="EDYTUJ">
            <input class="btn btn-dark" type="submit" value="USUŃ">
               </div></li>
        `;
        list.appendChild(row);
    }

    static clearFields() {
        document.querySelector('#imie').value = '';
        document.querySelector('#nazwisko').value = '';

    }

    clearAllFields() {
        document.querySelector('#imie').value = '';
        document.querySelector('#nazwisko').value = '';
        document.querySelector('#name').value = '';
        const row = document.querySelector('#studentList');
        row.innerHTML = '';
    }

    static displayClasses() {

        var tmp = new Class("tmp");
        tmp.ChangeName("1b");
        tmp.newStudent("Tomek", "Guz");
        tmp.newStudent("Ola", "Guz");
        UI.addClassToList(tmp);

        tmp.Clear();
    }

    static addClassToList(klasa) {

        const list2 = document.querySelector('.all-seperate');
        const row2 = document.createElement('button');
        row2.className = "unhiden_item dark-shadow";

        row2.innerHTML = `

        <ul class="item">
            <li>
                <p>${klasa.name}</p>
            </li>
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

        const row4 = document.createElement('div');
        row4.id = 'studentList';

        for (let i = 0; i < klasa.studentsArr.length; i++) {
            const row5 = document.createElement('ul');
            row5.innerHTML = `

            <li>${klasa.studentsArr[i].Imie} ${klasa.studentsArr[i].Nazwisko}</li>
            <li>
                <div class="btn-container">

                    <input class="btn btn-dark" type="submit" value="USUŃ">
                     </div>
                     </li>

                `;
            row4.appendChild(row5);
        }

        row3.appendChild(row4);
        list2.appendChild(row3);
    }
}

function initializeAddClass() {
    var addNew = document.querySelector('#addNew');
        addNew.addEventListener('click', (e) => {
            e.preventDefault();
            //const t = document.querySelector("#liczba");
            //t.style.visibility = "visible"; 
            const element = document.querySelector("#submitNewClass");
            element.style.visibility = "visible";

            const k = document.querySelector(".studentListContainer");
            k.style.visibility = "visible";
            k.style.height = "min-content";

            const imie = document.querySelector('#imie').value;
            const nazwisko = document.querySelector('#nazwisko').value;

            student = new Student(imie, nazwisko);

            studentsList.push(student);

            console.log(studentsList);

            UI.addStudentToList(student);

            UI.clearFields();
        });

    var addNewClass = document.querySelector('#addNewClass');
    addNewClass.addEventListener('click', (e) => {
        e.preventDefault();

        const element = document.querySelector(".addStudent");
        element.style.visibility = "visible";
        element.style.height = element.scrollHeight + 10 + "px";
        const k = document.querySelector("#addNewClass");
        k.style.visibility = "hidden";
        const s = document.querySelector("#submitNewClass");
        s.style.visibility = "hidden";
    });

    var addNewClassSubmit = document.querySelector('#submitNewClass');
        submitNewClass.addEventListener('click', (e) => {
            e.preventDefault();

            const name = document.querySelector('#name').value;
            // const studentsArr = document.querySelector('#nazwisko').value;

            let newclass = new Class(name);

            console.log(studentsList[0].imie);

            studentsList.forEach(student => {
                newclass.newStudent(student.Imie, student.Nazwisko);

                const s = document.querySelector("#submitNewClass");
                s.style.visibility = "hidden";

            });


            classesList.push(newclass);
            localStorage.setItem('MyClasses', JSON.stringify(classesList));

            UI.addClassToList(newclass);

            const k = document.querySelector(".studentListContainer");
            k.style.visibility = "hiddem";
            k.style.height = 0;
            const s = document.querySelector("#addNewClass");
            s.style.visibility = "visible";
            const element = document.querySelector(".addStudent");
            element.style.visibility = "hidden";

            var targetDiv = document.getElementById("name")
            targetDiv.value = '';
            studentsList = [];

            const row = document.querySelector('#studentList');
            row.innerHTML = '';
        });  
}



function initializeAddTeachers() {
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
}

function initializeAddClassrooms() {
    var addNew = document.querySelector('#submitNewClassroom');
    addNew.addEventListener('click', (e) => {
        e.preventDefault();

        const kod = document.querySelector('#kod').value;
        const nazwa = document.querySelector('#name').value;
        const ilosc_miejsc = document.querySelector('#count').value;

        klasa = new Classroom(kod, nazwa, ilosc_miejsc);

        clasroomsList.push(klasa);

        UI.addClassRoomToList(klasa);

        UI.clearFields();

        localStorage.setItem('MyClassrooms', JSON.stringify(clasroomsList));
    });
}









