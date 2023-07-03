export default {
    // logowanie 
    user(context, user) {
        context.commit('user', user)
    },
    patient(context, user) {
        context.commit('patient', user)
    },
    doctor(context, doctor) {
        context.commit('doctor', doctor)
    },
    // panel lekarza > lista pacjentów
    patientsList(context, patients){
        context.commit('patientsList', patients)
    },
    // panel recepcjonisty > lista wizyt
    visitsList(context, visits) {
        context.commit('visitsList', visits)
    },
    // panel recepcjonisty > modal
    activePatient(context, patient){
        context.commit('activePatient', patient)
    },
    // panel pacjenta > lista wizyt
    patientVisitsList(context, patientVisits){
        context.commit('patientVisitsList', patientVisits)
    },
    isLoggedIn(context, isLoggedIn) {
        context.commit('isLoggedIn', isLoggedIn)
    },
    accountType(context, type) {
        context.commit('accountType', type)
    },
};