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
    constructor(name, teacher) {
        this.id = classID++;
        this.name = name;
        this.teacher = teacher;
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

var retrievedData = localStorage.getItem("MyClasses");
var classes = JSON.parse(retrievedData);
function areData() {
    if (classes.length == []) {
        const bt = document.querySelector('#filldetails')
        console.log(bt);
        bt.disabled = "true";
    }
}
//tutaj jest sprawdzenie czy sa dane w localstorage

var groupSubjectId = 0;
let groupSubjectList = [];
class GroupSubject {
    constructor(n) {
        this.id = groupSubjectId++;
        this.studentsIdArr = [];
        this.divId = n;
        this.name = "group" + groupSubjectId;
        this.studentsName = [];
    }

    addStudent(i, name) {
        this.studentsIdArr.push(parseInt(i));
        this.studentsName.push(name);
    }

    removeStudent(i, name) {
        const index = this.studentsIdArr.indexOf(i);
        if (index > -1) {
            this.studentsIdArr.splice(index, 1);
        }
        const index2 = this.studentsName.indexOf(name);
        if (index2 > -1) {
            this.studentsName.splice(index2, 1);
        }
    }
    setName(n) {
        this.name = n;
    }

    setTeacher(n) {
        this.teacher = n;
    }

    setHours(i) {
        this.hours = i;
    }
}

let subjectId = 0;
let subjectList = [];
class Subject {
    constructor(name) {
        this.name = name;
        this.id = subjectId++;
    }

    setGroupSubjectList(array) {
        this.groupSubjectList = array.slice();
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

    static clearStudentForm() {
        document.querySelector('#imie').value = '';
        document.querySelector('#nazwisko').value = '';
    }

    static clearClassForm() {
        this.clearStudentForm();
        document.querySelector('#name').value = '';
        document.querySelector('#teacherSelect').value = '';
    }

    static clearClassRoomForm() {
        document.querySelector('#name').value = '';
        document.querySelector('#count').value = '';
        document.querySelector('#kod').value = '';
    }

    static clearTeacherForm() {
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
        const row2 = document.createElement('ul');
        row2.className = "item";

        row2.innerHTML = `
             <li> <p>${klasa.kod}</p></li>
             <li> <p>${klasa.nazwa}</p></li>
             <li> <p>${klasa.ilosc_miejsc}</p> </li>
             <li> <div class="btn-container">
              <input class="btn btn-dark" type="submit" value="USUŃ">
                   </div>
            </li>
        `;

        list2.appendChild(row2);
    }
    static clearFields() {
        document.querySelector('#kod').placeholder = 'kod';
        document.querySelector('#name').placeholder = 'nazwa';
        document.querySelector('#count').placeholder = '1';
    }
    static clearAllSubjectFields() {
        document.querySelector('#nameSubject').value = '';
        document.querySelector('#division').checked = false;
        document.querySelector('#group').value = '';
        const s = document.querySelector(".divisionStudent");
        s.style.maxHeight = null;
        s.style.padding = "0";

        const el = document.querySelectorAll(".selectable");
        el.forEach(e => {
            e.removeAttribute("disabled");

            var addGroups = document.querySelector('#filldetails');
            var back = document.querySelector('#fillback');

            addGroups.style.visibility = "visible";
            back.style.visibility = "hidden";

            const el = document.querySelectorAll("#hidden-element");
            el.forEach(e => {
                e.style.visibility = "hidden";
            });
        });
    }

    static deleteStudent(element) {
        if (element.classList.contains('delete'))
            element.parentElement.parentElement.parentElement.remove();
    }

    static deleteClass(element) {
        if (element.classList.contains('delete')) {
            element.parentElement.parentElement.parentElement.remove();
        }
    }
    static deleteSubject(element) {
        if (element.classList.contains('delete')) {
            element.parentElement.parentElement.parentElement.remove();
        }
    }

    static deletePanelClass(element, id) {
        if (element.classList.contains('delete')) {
            var el = document.getElementById(id + "panel");
            el.remove();
            var e = document.getElementById(id + "item");
            e.remove();
        }
    }

    static addStudentToList(student) {
        const list = document.querySelector('#studentList');

        const row = document.createElement('ul');
        row.className = "dark-shadow";

        row.innerHTML = `
         <li>${student.imie} ${student.nazwisko}</li>
         <li><div class="btn-container">
         <input id=${student.id} class="btn btn-dark delete" type="submit" value="USUŃ">
         </div></li>`;
        row.addEventListener('click', (e) => {
            UI.deleteStudent(e.target);
            let I = e.target.id;
            for (var i = 0; i < studentsList.length; i++) {
                if (studentsList[i].Id == I) { studentsList.splice(i, 1); }
            }
        });
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
        row2.id = klasa.id + "item";

        row2.innerHTML = `
                <ul class="item">
                            <li>
                            <p>${klasa.name}</p>
                            </li>
                            <li>
                            <p>${klasa.teacher}</p>
                            </li>
                            <li>
                                <div class="btn-container">
                                 <input id=${klasa.id} class="btn btn-dark delete" type="submit" value="USUŃ">
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

        //usuwanie nauczyciela i update tablicy nauczyciela, usuniecie schowanego panelu

        row2.addEventListener('click', (e) => {
            UI.deleteClass(e.target);
            let s = e.target.id;

            UI.deletePanelClass(e.target, s);
            for (var i = 0; i < classesList.length; i++) {
                if (classesList[i].id == s) { classesList.splice(i, 1); }
            }

            localStorage.setItem('MyClasses', JSON.stringify(classesList));
        });

        list2.appendChild(row2);

        const row3 = document.createElement('div');
        row3.className = 'panel';
        row3.id = klasa.id + "panel";

        const row4 = document.createElement('div');
        row4.id = 'studentList';
        let i;
        for (i = 0; i < klasa.studentsArr.length; i++) {
            const row5 = document.createElement('ul');
            row5.className = "dark-shadow";
            row5.innerHTML = `
                    <li>${klasa.studentsArr[i].Imie} ${klasa.studentsArr[i].Nazwisko}</li>
                    <li>
                    <div class="btn-container">
                    <input id=${klasa.studentsArr[i].id} class="btn btn-dark  delete" type="submit" value="USUŃ">
                     </div>
                     </li>
                    `;

            row5.addEventListener('click', (e) => {
                UI.deleteStudent(e.target);
                let I = e.target.id;
                for (var i = 0; i < klasa.studentsArr.length; i++) {
                    if (klasa.studentsArr[i].Id == I) { klasa.studentsArr.splice(i, 1); }
                }

                localStorage.setItem('MyClasses', JSON.stringify(classesList));
            });
            row4.appendChild(row5);
        }

        row3.appendChild(row4);

        list2.appendChild(row3);
    }

    static addSubjectToList(newSubject) {
        const list2 = document.querySelector('.all-seperate');
        const row2 = document.createElement('button');
        row2.className = "unhiden_item dark-shadow";
        row2.id = newSubject.id + "item";

        row2.innerHTML = `
                <ul class="item">
                <li>
                <p>${newSubject.name}</p>
                </li>
                <li>
                <div class="btn-container">
                <input id=${newSubject.id} class="btn btn-dark delete" type="submit" value="USUŃ">
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

        row2.addEventListener('click', (e) => {
            UI.deleteSubject(e.target);
            let s = e.target.id;

            UI.deletePanelClass(e.target, s);
            for (var i = 0; i < subjectList.length; i++) {
                if (subjectList[i].id == s) { subjectList.splice(i, 1); }
            }

            localStorage.setItem('MySubjects', JSON.stringify(subjectList));
        });

        list2.appendChild(row2);

        const row3 = document.createElement('div');
        row3.className = 'panel';
        row3.id = newSubject.id + "panel";

        const row4 = document.createElement('div');
        row4.id = 'groupSubjectList';
        let i;

        if (newSubject.groupSubjectList.length > 1) {
            for (i = 0; i < newSubject.groupSubjectList.length; i++) {
                const list = document.createElement('div');
                list.id = i;
                list.className = "groupAllInfo dark-shadow"

                const row = document.createElement('div');
                row.className = "groupDetails"
                row.innerHTML = `
                            <p>Nazwa grupy:</p>
                            <p>${newSubject.groupSubjectList[i].name} </p>
                            <p>Nauczyciel:</p>
                            <p>${newSubject.groupSubjectList[i].teacher}</p>
                            <p>Ilość godzin:</p>
                            <p>${newSubject.groupSubjectList[i].hours}</p> `;
                list.appendChild(row);

                const r = document.createElement('div');
                r.className = "groupStudents"
                const row5 = document.createElement('div');

                row5.innerHTML = `<h3>LISTA UCZNIÓW</h3>`;
                r.appendChild(row5);

                for (let j = 0; j < newSubject.groupSubjectList[i].studentsName.length; j++) {
                    const row6 = document.createElement('div');
                    row6.innerHTML = `
                                 <p>${newSubject.groupSubjectList[i].studentsName[j]}</p> `;
                    r.appendChild(row6);
                }

                list.appendChild(r);

                row4.appendChild(list);
            }
            row3.appendChild(row4);
            list2.appendChild(row3);
        }

        else {
            const list = document.createElement('div');
            list.id = i;
            list.className = "groupAllInfo dark-shadow"

            const row = document.createElement('div');
            row.className = "groupDetails"
            row.innerHTML = `
                            <p>Nauczyciel:</p>
                            <p>${newSubject.groupSubjectList[0].teacher}</p>
                            <p>Ilość godzin:</p>
                            <p>${newSubject.groupSubjectList[0].hours}</p> `;
            list.appendChild(row);

            row3.appendChild(list);
            list2.appendChild(row3);
        }
    }
}
function removeStudent(id_div, id_student, name) {
    for (let k = 0; k < groupSubjectList.length; k++) {
        if (groupSubjectList[k].divId == id_div) {
            groupSubjectList[k].removeStudent(id_student, name);
            break;
        }
    }
}

function addStudent(id_div, id_student, name) {
    for (let k = 0; k < groupSubjectList.length; k++) {
        if (groupSubjectList[k].divId == id_div) {
            groupSubjectList[k].addStudent(id_student, name);
            break;
        }
    }
}

function setTeacher(id_div, name) {
    for (let k = 0; k < groupSubjectList.length; k++) {
        if (groupSubjectList[k].divId == id_div) {
            groupSubjectList[k].setTeacher(name);
            break;
        }
    }
}

function setName(id_div, value) {
    for (let k = 0; k < groupSubjectList.length; k++) {
        if (groupSubjectList[k].divId == id_div) {
            groupSubjectList[k].setName(value);
            break;
        }
    }
}

function setHours(id_div, value) {
    for (let k = 0; k < groupSubjectList.length; k++) {
        if (groupSubjectList[k].divId == id_div) {
            groupSubjectList[k].setHours(value);
            break;
        }
    }
}

function displayStudents() {
    const list = document.querySelector('.studentList');
    list.style.visibility = "visible";
    while (list.hasChildNodes()) {
        list.removeChild(list.lastChild);
    }

    var retrievedData = localStorage.getItem("MyStudents");
    var classes = JSON.parse(retrievedData);
    const row2 = document.createElement('ul');
    const row4 = document.createElement('li');
    row4.innerHTML =
        `<p>Uczniowie</p>`
    row2.appendChild(row4);
    //dla mnie dodałam sobie 0, ale ma być to id wybranej klasy
    for (let i = 0; i < classes.length; i++) {
        const row3 = document.createElement('li');
        row3.className = "dark-shadow";
        row3.id = "studentDraggable";
        row3.style.background = "rgb(67,113,98)";
        row3.setAttribute("draggable", "true");
        row3.innerHTML = `
            <p id=${classes[i].Id}>${classes[i].FirstName} ${classes[i].LastName}</p>`

        row3.addEventListener("dragstart", dragStart);
        row3.addEventListener("dragend", dragEnd);
        row2.appendChild(row3);
    }

    list.appendChild(row2);
}

function setGroupSubjectOfAllStudents() {
    var retrievedData = localStorage.getItem("MyStudents");
    var classes = JSON.parse(retrievedData);
    let group = new GroupSubject("1");
    for (let i = 0; i < classes.length; i++) {
        let id = parseInt(classes[i].Id);
        let name = classes[i].FirstName + " " + classes[i].LastName;
        group.addStudent(id, name);
    }

    groupSubjectList.push(group);

    return group;
}

let draggableStudent = null;

//dodawanie dynamicznie ilosci grup i tworzenie grup
function addGroupSubjectFields() {
    var number = document.getElementById("group").value;
    var container = document.getElementById("groupSubjectList");

    while (container.hasChildNodes()) {
        container.removeChild(container.lastChild);
        groupSubjectList = [];
    }

    let NumberGroupSubject = 0;

    for (i = 1; i <= number; i++) {
        let g = new GroupSubject(i);
        groupSubjectList.push(g);
        const list = document.createElement('div');
        list.id = i;
        list.className = "groupName";
        const row = document.createElement('div');
        row.className = "groupDetails"
        row.innerHTML = `
        <label for=${i}>Nazwa grupy:</label>
        <input id=${i} type="text" placeholder="Grupa ${i}" onchange ="setName(this.id, this.value)">
        <label for=${i}>Nauczyciel:</label>
        <input id=${i} type="text" placeholder="Podaj nauczyciela" onchange ="setTeacher(this.id, this.value)">
        <label for=${i}>Ilość godzin w tygodniu:</label>
        <input id=${i} type="number" placeholder="1" onchange ="setHours(this.id, this.value)">
        `;
        list.appendChild(row);
        const row2 = document.createElement('div');
        row2.innerHTML = `<h4>Lista uczniów</h4>`;
        list.appendChild(row2);
        list.addEventListener("dragover", dragOver);
        list.addEventListener("drop", dragDrop);
        container.appendChild(list);
    }
}

function displayTeacherFiels() {
    var container = document.querySelector(".addTeacher");
    container.style.visibility = "visible";
    container.style.gridColumn = "1 / span 4";
    container.style.display = "flex";
    container.style.height = "min-height";
    while (container.hasChildNodes()) {
        container.removeChild(container.lastChild);
    }
    container.style.gridTemplateColumns = "auto";

    const row = document.createElement('div');
    row.className = "groupDetails"
    row.innerHTML = `
        <label for="1">Nauczyciel:</label>
        <input id="1" type="text" placeholder="Podaj nauczyciela" onchange ="setTeacher(this.id, this.value)">
        <label for="1">Ilość godzin w tygodniu:</label>
        <input id="1" type="number" placeholder="1" onchange ="setHours(this.id, this.value)">
        `;
    container.appendChild(row);
    const bt = document.querySelector(".btnNext");
    bt.style.visibility = "visible";
    const b = document.querySelector("#addSubject");
    b.removeAttribute("disabled");
    bt.style.display = "flex";
}

function checkStudentsList() {
    const el = document.querySelector(".studentList ul");
    if (el.children.length == 1) {
        const bt = document.querySelector(".btnNext");
        const b = document.querySelector("#addSubject");
        b.removeAttribute("disabled");
        bt.style.visibility = "visible";
        bt.style.display = "flex";
    }
}

function dragStart() {
    draggableStudent = this;
    // console.log(this.parentElement.id);
    //console.log(this.children[0].id)
    let name = this.children[0].innerHTML;
    removeStudent(this.parentElement.id, this.children[0].id, name); //id_div,id_ucznia
}

function dragEnd() {
    draggableStudent = null;
    //console.log(this.parentElement.id);
    //console.log(this.children[0].id)
    let name = this.children[0].innerHTML;
    addStudent(this.parentElement.id, this.children[0].id, name); //id_div,id_ucznia
}
function dragOver(e) {
    e.preventDefault();
}
function dragDrop() {
    this.appendChild(draggableStudent);
    checkStudentsList();
    //sprawdza czy lista uczniow jest pusta, jesli tak można dodawać nauczycieli
}

//dodawanie divow dla grupy, lub dodanie nauczyczyciela do klasy, jeżeli nie ma grup, to grupa to cała klasa

//odpowiada za pojawianie sie możliwości dodawania grupy, jeśli jest chcech, to przycisk submit musi pokazywać podział grup,
//jeżeli nie, to grupa to cała klasa, a submit pokazuje tylko opcje dodawania nauczycieli;
function divisionOn(elem) {
    const el = document.querySelectorAll("#hidden-element");

    if (elem.checked) {
        el.forEach(e => {
            e.style.visibility = "visible";
        });
    } else {
        el.forEach(e => {
            e.style.visibility = "hidden";
        });
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
        UI.clearStudentForm();
    });

    var addNewClass = document.querySelector('#addNewClass');
    addNewClass.addEventListener('click', (e) => {
        e.preventDefault();

        const element = document.querySelector(".addStudent");
        element.style.visibility = "visible";
        element.style.padding = " 0 16px"
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
        const teacher = document.querySelector('#teacherSelect').value;
        // const studentsArr = document.querySelector('#nazwisko').value;

        let newclass = new Class(name, teacher);

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
        element.style.padding = "0 0";
        element.style.height = "0";
        studentsList = [];

        const row = document.querySelector('#studentList');
        row.innerHTML = '';

        UI.clearClassForm();
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
        localStorage.setItem('MyTeachers', JSON.stringify(nauczycieleList));
        UI.clearTeacherForm();
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

        localStorage.setItem('MyClassrooms', JSON.stringify(clasroomsList));
        UI.clearClassRoomForm();
    });
}

function initializeSubjects() {
    var submitNewSubject = document.querySelector('#addSubject');
    submitNewSubject.addEventListener('click', (e) => {
        e.preventDefault();

        const name = document.querySelector('#nameSubject').value;

        let newSubject = new Subject(name);

        console.log(groupSubjectList);

        newSubject.setGroupSubjectList(groupSubjectList);

        subjectList.push(newSubject);

        localStorage.setItem('MySubjects', JSON.stringify(subjectList));

        const bt = document.querySelector(".btnNext");
        bt.disabled = true;
        //bt.style.visibility = "hidden";
        bt.style.display = "none";

        var container = document.querySelector(".addTeacher");
        container.style.visibility = "hidden";
        container.style.maxHeight = null;
        container.style.display = "none";

        UI.addSubjectToList(newSubject);
        UI.clearAllSubjectFields();
        groupSubjectList = [];
    });

    var addGroupSubjects = document.querySelector('#filldetails');
    var back = document.querySelector('#fillback');
    const s = document.querySelector(".divisionStudent");
    addGroupSubjects.addEventListener('click', (e) => {
        e.preventDefault();
        addGroupSubjects.style.visibility = "hidden";
        back.style.visibility = "visible";
        s.style.gridTemplateColumns = "1fr 6fr";

        const el = document.querySelectorAll(".selectable");
        el.forEach(e => {
            e.disabled = "true";
        });

        const ch = document.querySelector('#division')
        if (ch.checked) {
            addGroupSubjectFields();
            displayStudents()
            var r = document.querySelector("#groupSubjectList").style.height;
            console.log(r);
            s.style.height = "auto";
            s.style.maxHeight = "min-content";
            s.style.padding = "2rem";
            const bt = document.querySelector(".btnNext");
            const b = document.querySelector("#addSubject");
            b.disabled = true;
            bt.style.visibility = "visible";
            bt.style.display = "flex";
        }
        else { //jezeli nie ma podzialu
            let oneGroupSubject = setGroupSubjectOfAllStudents();
            displayTeacherFiels();
        }
    });

    back.addEventListener('click', (e) => {
        e.preventDefault();
        back.style.visibility = "hidden";
        addGroupSubjects.style.visibility = "visible";
        s.style.maxHeight = null;
        s.style.padding = "0";
        const bt = document.querySelector(".btnNext");
        const b = document.querySelector("#addSubject");
        b.disabled = true;
        // bt.style.visibility = "hidden";
        bt.style.display = "none";
        const el = document.querySelectorAll(".selectable");
        el.forEach(e => {
            e.removeAttribute("disabled");
        });

        const st = document.querySelector(".studentList");

        st.style.visibility = "hidden";
        st.style.maxHeight = "min-content";
        st.style.padding = "1rem";

        var container = document.querySelector(".addTeacher");
        container.style.visibility = "hidden";
        container.style.maxHeight = null;
        container.style.display = "none";
    });
}