<template>
    <section id="patient-prescriptions">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <breadcrumbs is-patient>
                        <router-link to="/recepty">Recepty</router-link>
                    </breadcrumbs>
                    <hello-message v-if='user' :name="user.firstName" icon-name="receipt"><template v-slot:info>Oto lista Twoich recept wraz z zaleceniami lekarskimi</template></hello-message>
                    <div class="row" v-if="patient.prescriptions.length > 0">
                        <div class="col-md-6" v-for="prescription in patient.prescriptions" :key="prescription" @click="openPrescriptionModal(prescription)">
                            <base-card v-if='prescription' class="base-card">
                                <template v-slot:title>Recepta z dnia {{ new Date(prescription.date).toLocaleDateString('pl', { year:"numeric", month:"long", day:"numeric"}) }}</template>
                                <template v-slot:content>Wystawca: {{ prescription.doctor.user.firstName }} {{ prescription.doctor.user.lastName }} ({{ prescription.doctor.specialization }})</template>
                            </base-card>
                        </div>
                    </div>
                    <div class='no-results' v-else>
                        <p>Nie masz wystawionych recept</p>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <prescription-modal v-if="prescriptionModalIsOpen" :data="prescriptionModalData" @close-prescription-modal="prescriptionModalIsOpen = false"></prescription-modal>
</template>

<script>
import { mapGetters } from 'vuex';
import axios from 'axios';
import jwt_decode from "jwt-decode";
import PrescriptionModal from "@/components/PrescriptionModal.vue"

export default {
    data(){
        return {
            prescriptionModalData: [],
            prescriptionModalIsOpen: false,
        }
    },
    components: {
        PrescriptionModal
    },
    methods: {
        openPrescriptionModal(prescription) {
            this.prescriptionModalData = prescription;
            return this.prescriptionModalIsOpen = true;
        }
    },
    computed: {
        ...mapGetters(['user', 'patient'])
    },
    async created(){
      const token = localStorage.getItem('token');
      const tokenDecoded = jwt_decode(token);
      const getPatientInfo = await axios.get(`https://localhost:7121/api/Uzytkownik/Dane-personalne/${tokenDecoded.roleId}`);
      await this.$store.dispatch('patient', getPatientInfo.data);

      console.log(getPatientInfo)
    },
}
</script>

<style lang="scss" scoped>
div.container {
    div.base-card {
        padding: 0 20px;
        margin: 30px 0;
    }
}

div {
    &.no-results {
        p {
            font-size: 30px;
        }
    }
}
        
</style>