const apiUrl = "http://localhost:5074/api/Tasks";

const taskList = document.getElementById("taskList");
const taskInput = document.getElementById("taskInput");
const addTaskBtn = document.getElementById("addTaskBtn");

async function loadTasks() {
    const response = await fetch(apiUrl);
    const tasks = await response.json();

    taskList.innerHTML = "";

    for (const task of tasks) {
        const li = document.createElement("li");

        const span = document.createElement("span");
        span.textContent = task.title;
        span.className = "task-title";
        if (task.isDone) {
            span.classList.add("done");
        }

        const deleteBtn = document.createElement("button");
        deleteBtn.textContent = "Delete";
        deleteBtn.addEventListener("click", async () => {
            await deleteTask(task.id);
        });

        li.appendChild(span);
        li.appendChild(deleteBtn);
        taskList.appendChild(li);
    }
}

async function addTask() {
    const title = taskInput.value.trim();

    if (title === "") {
        return;
    }

    const newTask = {
        title: title,
        isDone: false
    };

    await fetch(apiUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(newTask)
    });

    taskInput.value = "";
    await loadTasks();
}

async function deleteTask(id) {
    await fetch(`${apiUrl}/${id}`, {
        method: "DELETE"
    });

    await loadTasks();
}

addTaskBtn.addEventListener("click", addTask);

loadTasks();