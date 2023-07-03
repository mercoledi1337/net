import actions from "./actions.js"
import getters from "./getters.js"
import mutations from "./mutations.js"

export default {
    state(){
        return {
            user: null,
            patient: null,
            doctor: null,
            accountType: null,
            isLoggedIn: false,
            patientsList: [],
            visitsList: [],
            activePatient: null,
            patientVisitsList: [],
        }
    },
    actions, 
    getters,
    mutations,
}