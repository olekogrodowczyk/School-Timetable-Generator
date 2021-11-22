
    localStorage.clear();



    var studentId = 0;
    var studentsList = [];
    class Student {

        constructor(imie, nazwisko) {
        this.id = studentId++;
    this.imie = imie;
    this.nazwisko = nazwisko;
            }


    get Imie(){
                return this.imie;
            }
    get Nazwisko(){
                return this.nazwisko;
            }
            
        }

    var classID = 0 ;
    let classesList = [];

    class Class{
        constructor(name){
        this.id = classID++;
    this.name=name;
    this.studentsArr = [];
            }
    newStudent(imie, nazwisko){
        let s = new Student(imie, nazwisko);
    this.studentsArr.push(s);
    return s;
            }

    allStudents(){
             return this.studentsArr
            }

    get numberOfPlayers(){
            return this.studentsArr.length
            }

    ChangeName(name){
        this.name = name;
            }

    Clear(){
        this.studentsArr = [];
            }

    Reset(){
        this.name = tmp;
    this.id=classID++;
    this.studentsArr = [];
            }

        }

    class UI {
        static displayStudents(){
    }

    static addStudentToList(student){

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

        static clearFields(){
            document.querySelector('#imie').value = '';
        document.querySelector('#nazwisko').value= '';

            }

        clearAllFields(){
            document.querySelector('#imie').value = '';
        document.querySelector('#nazwisko').value= '';
        document.querySelector('#name').value ='';
        const row = document.querySelector('#studentList');
        row.innerHTML = '';
            }

        static displayClasses(){

                var tmp = new Class("tmp");
        tmp.ChangeName("1b");
        tmp.newStudent("Tomek", "Guz");
        tmp.newStudent("Ola", "Guz");
        UI.addClassToList(tmp);

        tmp.Clear();
            }

        static addClassToList(klasa){

                const list2 = document.querySelector('.all-seperate');
        const row2 = document.createElement('button');
        row2.className = "unhiden_item dark-shadow";

        row2.innerHTML= `

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
            row2.addEventListener("click", function() {
                this.classList.toggle("active");
            var panel = this.nextElementSibling;
            if (panel.style.maxHeight) {
                panel.style.maxHeight = null;
            } else {
                panel.style.maxHeight = panel.scrollHeight + "px";
            }});

            list2.appendChild(row2);

            const row3=document.createElement('div');
            row3.className='panel';

            const row4 = document.createElement('div');
            row4.id='studentList';

            for(let i=0; i<klasa.studentsArr.length; i++){
                const row5 = document.createElement('ul');
            row5.innerHTML= `

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


var addNew = document.querySelector('#addNew');
if (addNew) {
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
}

var addNewClass = document.querySelector('#addNewClass');
if (addNew) {
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
}

            
var addNewClassSubmit = document.querySelector('#submitNewClass');
if (addNewClassSubmit) {

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