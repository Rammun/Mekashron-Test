import {createRouter, createWebHistory} from "vue-router";
import Menu2 from "@/components/Menu2.vue";
import Menu3 from "@/components/Menu3.vue";
import LoginForm from "@/components/LoginForm.vue";

const routes = [
    {
        path: "/",
        name: "LoginForm",
        component: LoginForm,
    },
    {
        path: "/Menu2",
        name: "Menu2",
        component: Menu2,
    },
    {
        path: "/Menu3",
        name: "Menu3",
        component: Menu3,
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;