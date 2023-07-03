<template>
    <section id="patients-list">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <breadcrumbs>
                        <router-link to="/pacjenci">Pacjenci</router-link>
                    </breadcrumbs>
                    <hello-message v-if="user" :name="user.firstName" icon-name="agenda"><template v-slot:info>Oto lista zarezerwowanych wizyt przez pacjentów</template></hello-message>         
                    <div class="wrapper d-flex flex-column">
                        <div class="search">
                            <div class="input-group mb-5">
                                <input type="search" class="form-control" v-model='searchQuery' placeholder="Wyszukaj...">
                            </div>
                        </div>
                        <div class="table-responsive d-flex flex-column">  
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th v-for="field in fields" :key='field'> {{ capitalizeFirstLetter(field) }} </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="patient in patients" :key="patient.id">
                                        <td v-for="field in fields" :key='field'> 
                                            <span v-if="field == 'pacjent'">
                                                <span>
                                                    <img src="@/assets/images/icons/svg/profile.svg">
                                                </span>
                                                <span>
                                                    {{ capitalizeFirstLetter(patient.user.firstName) }} {{  capitalizeFirstLetter(patient.user.lastName) }}   
                                                </span>
                                            </span>
                                            <span v-else>
                                                <span >
                                                    <button @click="openPatientCardModal(patient)" class="blue-button">Karta pacjenta</button>
                                                    <button @click="openWritePrescriptionModal(patient)" class="teal-button">Wypisz receptę</button>
                                                </span>
                                            </span> 
                                        </td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th>
                                            <span class="tfoot-icon">
                                                <img src="@/assets/images/icons/svg/table_arrow_left.svg">
                                            </span>
                                            <span class="tfoot-text">Poprzednia strona</span></th>
                                        <th v-for="field in fields.length-2" :key="field"></th>  
                                        <th>
                                            <span class="tfoot-text">Następna strona</span>
                                            <span class="tfoot-icon">
                                                <img src="@/assets/images/icons/svg/table_arrow_right.svg">
                                            </span>
                                        </th>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <patient-card-modal @close-patient-card-modal="patientCardModalIsOpen = false" v-if="patientCardModalData && patientCardModalIsOpen" :data="patientCardModalData"></patient-card-modal>
    <write-prescription-modal @close-write-prescription-modal="writePrescriptionModalIsOpen = false" v-if="writePrescriptionModalIsOpen" :data="writePrescriptionModalData" :doctor-id-prop="doctor.id"></write-prescription-modal>
</template>

<script>
import axios from 'axios'
import { mapGetters } from 'vuex'
import { computed, ref } from "vue";
import jwt_decode from "jwt-decode";
import PatientCardModal from '@/components/PatientCardModal.vue';
import WritePrescriptionModal from '@/components/WritePrescriptionModal.vue';

export default {
    data(){
        return {
            patients: [],
            fields: ['pacjent', 'akcje'],
            //patientInfo: [],
            patientCardModalData: [],
            patientCardModalIsOpen: false,
            writePrescriptionModalData: [],
            writePrescriptionModalIsOpen: false,
        }
    },
    components: {
        PatientCardModal,
        WritePrescriptionModal
    },
    methods: {
        openPatientCardModal(patientInfo) {
            this.patientCardModalData = patientInfo;
            this.patientCardModalIsOpen = true;
        },
        openWritePrescriptionModal(patientInfo){
            this.writePrescriptionModalData = patientInfo;
            this.writePrescriptionModalIsOpen = true;
        }
    },
    computed: {
        ...mapGetters(['user', 'doctor', 'isLoggedIn'])
    },

    setup(props) {
        let sort = ref(false);
        let updatedList =  ref([])
        let searchQuery = ref("");
        
        const sortedList = computed(() => {
            if (sort.value) {
                return updatedList.value
            } else {
                return props.data;
            }
        });

        const filteredList = computed(() => {
            return sortedList.value.filter((product) => {
                if(product.pacjent){
                    return product.pacjent.toLowerCase().indexOf(searchQuery.value.toLowerCase()) != -1;
                }
                return product;
                
            });
        });   
    
        return { sortedList, searchQuery, filteredList }
    },
    async created(){
        const token = localStorage.getItem('token');
        const tokenDecoded = jwt_decode(token);
        console.log(tokenDecoded)
        const responseUserId = await axios.get(`https://localhost:7121/api/Uzytkownik/Dane-personalne/${tokenDecoded.nameid}`);
        const responseDoctorId = await axios.get(`https://localhost:7121/api/Uzytkownik/lekarze/${tokenDecoded.roleId}`);
        console.log(responseUserId)
        await this.$store.dispatch('user', responseUserId.data);
        await this.$store.dispatch('doctor', responseDoctorId.data);
    
    },
    async mounted(){
        const patientInfo = await axios.get('https://localhost:7121/api/Lekarz/pacjenci');
        this.patients = patientInfo.data;
        console.log(patientInfo.data)
    },
}
</script>

<style lang="scss" scoped>

    div.wrapper {
    margin: 40px 0;
    div.search {
        width: 300px;
        align-self: flex-end;

        @media (max-width: 768px) { 
            align-self: center;
        }

        div.input-group {
            input {
                background-image: url("@/assets/images/icons/svg/magnifying_glass.svg");
                background-repeat: no-repeat;
                background-position-x: 16px;
                background-position-y: 8px;
                background-color: $primary;
                border: 1px solid $secondary;
                padding-left: 48px;
                font-weight: 600;
                color: $secondary;

                &:focus {
                    box-shadow: rgba(0, 0, 0, 0.2) 0px 2px 2px;
                }
            }
        }
    }
    div.table-responsive {
        border: 1px solid $button-light;
        border-radius: 10px;
        background-color: $primary;

        table {
            margin: 0;
            
            thead, tfoot, tbody {
                tr {
                    th, td {
                        text-align: left;
                    }
                }
            }
            thead, tfoot {
                tr {
                    &:hover {
                        background-color: transparent;
                    }
                    th {
                        padding: 24px;
                        font-weight: 600;
                        line-height: 18px;
                        color: $secondary;

                        &:last-child {
                            width: 200px;
                        }
                    }
                }
            }
            thead {
                tr {
                    border-radius: 0 15px 0 15px;
                }
            }
            tbody {
                tr {
                    &:hover {
                        background-color: $button-light-hover;
                    }
                    td {
                        padding: 16px 24px;
                        font-weight: 500;
                        vertical-align: middle;
                        font-size: 14px;
                        white-space: nowrap;
                        

                        span {
                            span {
                                vertical-align: middle;

                                &:first-child {
                                    margin-right: 16px;
                                }
                                &.status-icon {
                                    background-color: setStatus;
                                    width: 15px;
                                    height: 15px;
                                    display: inline-block;
                                    box-shadow: inset 0px 4px 4px rgba(0, 0, 0, 0.25);
                                    border-radius: 100%;
                                    box-sizing: border-box;
                                    margin-right: 10px;
                                    
                                }
                            }
                        }
                        
                        button {
                            border-radius: 5px;
                            padding: 2px 8px;
                            color: white;
                            font-weight: 500;
                            font-size: 13px;
                            line-height: 18px;
                            border: 0;
                            margin-right: 20px;
                            transition: all .2s ease-in-out;

                            &.blue-button {
                                background-color: $button-blue;

                                &:hover {
                                    background-color: $button-blue-hover;
                                }
                            }

                            &.teal-button {
                                background-color: $button-teal;

                                &:hover {
                                    background-color: $button-teal-hover;
                                }
                            }

                            &.red-button {
                                background-color: $button-red;

                                &:hover {
                                    background-color: $button-red-hover;
                                }
                            }       
                        }
                    }
                }
            }

            tfoot {
                tr {
                    th {
                        border: 0;
                        white-space: nowrap;
                        span.tfoot-text {
                            vertical-align: middle;
                            margin: 0 6px;
                        }
                        &:last-child {
                            text-align: right;
                        }
                    }
                }
            }
        }
    }
}
</style>