import { createRouter, createWebHistory } from "vue-router";
import { useToast } from "vue-toastification";

import HomeView from "../views/HomeView.vue";
import SignUpView from "../views/SignUpView.vue";
import LoginView from "../views/LoginView.vue";
// import store from "@/store/index.js"

// pacjent
import PatientMakeAnAppointment from "../views/patient/PatientMakeAnAppointment.vue";
import PatientPrescriptions from "../views/patient/PatientPrescriptions.vue"
import PatientVisits from "../views/patient/PatientVisits.vue";
import PatientProfile from "../views/patient/PatientProfile.vue";
import PatientProfileEdit from "../views/patient/PatientProfileEdit.vue";
import PatientProfileEditPassword from "../views/patient/PatientProfileEditPassword.vue";

// pracownik
import ReceptionistPatientsVisits from "../views/employee/ReceptionistPatientsVisits.vue";
import PatientsList from "../views/employee/PatientsList.vue";

const routes = [
  {
    path: "/",
    name: "home",
    component: HomeView,
  },
  {
    path: "/logowanie",
    name: "login",
    component: LoginView,
  },
  {
    path: "/rejestracja",
    name: "signup",
    component: SignUpView,
  },
  // pacjent
  {
    path: "/moje-wizyty",
    name: "patient-visits",
    component: PatientVisits,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/recepty",
    name: "patient-prescriptions",
    component: PatientPrescriptions,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/umow-wizyte",
    name: "patient-make-an-appointment",
    component: PatientMakeAnAppointment,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/profil",
    name: "patient-profile",
    component: PatientProfile,
  },
  {
    path: "/profil/edycja-profilu",
    name: "patient-profile-edit",
    component: PatientProfileEdit,
    meta: {
      requiresAuth: true,
    }
  },
  {
    path: "/profil/edycja-hasla",
    name: "patient-profile-edit-password",
    component: PatientProfileEditPassword,
    meta: {
      requiresAuth: true
    }
  },

  // pracownik
  {
    path: "/wizyty",
    name: "receptionist-patients-visits",
    component: ReceptionistPatientsVisits,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: "/pacjenci",
    name: "patients-list",
    component: PatientsList,
    meta: {
      requiresAuth: true
    }
  },

];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
  scrollBehavior() {
    return { top: 0, left: 0 }
  }
});

router.beforeEach((to) => {
  if(to.meta.requiresAuth && !localStorage.getItem('token')){
    const toast = useToast();
    toast.error("Ta akcja wymaga autoryzacji", {
      timeout: 2500,
      position: "bottom-right",
    });
    return  { name: "login" } ;
  }
  return true

  
})

export default router;
