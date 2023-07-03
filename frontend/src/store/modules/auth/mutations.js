export default {
    user (state, user) {
        state.user = user;
    },
    patient (state, patient) {
        state.patient = patient;
    },
    doctor(state, doctor) {
        state.doctor = doctor;
    },
    patientsList (state, patients) {
        state.patientsList = patients
    },
    visitsList (state, visits) {
        state.visitsList = visits
    },
    activePatient (state, patient){
        state.activePatient = patient 
    },
    patientVisitsList (state, patientVisits) {
        state.patientVisitsList = patientVisits
    },
    isLoggedIn(state, isLoggedIn){
        state.isLoggedIn = isLoggedIn;
    },
    accountType(state, type) {
        state.accountType = type
    },
};