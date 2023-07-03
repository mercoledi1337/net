<template>
  <section id="home">
    <div class="container">
      <div class="row">
        <div class="col-md-6 offset-md-3">
          <h1>Przychodnia studencka</h1>
          <p class="home-text">Z myślą o Twoim komforcie i wygodzie, oferujemy możliwość umówienia się
          na wizytę online. Wystarczy, że zalogujesz się na swoje konto pacjenta na
          naszej stronie internetowej, a następnie wybierzesz dogodny termin i
          godzinę wizyty. Nasz system potwierdzi Twoją rezerwację i przypomni Ci o
          niej przed planowanym terminem.</p>
          <div v-if="!user" class="home-buttons mt-3 mb-5">
            <router-link to="/logowanie"><base-button type="dark" class="mx-2">Logowanie</base-button></router-link>
            <router-link to="/rejestracja"><base-button class="mx-2" style="background-color: #E6E9EC; border: 1px solid #D1D9E2; color: rgb(95, 109, 126);">Rejestracja</base-button></router-link>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-md-12">
          <img class="img-fluid mt-4" src="../assets/images/banner.png" alt="Banner">
        </div>
      </div>
    </div>
  </section>
  <section id="specjalisci">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <h2 class="mt-5">Nasi specjaliści</h2>
          <p>Umów wizytę do wybranego lekarza</p>
          <div class="najblizsze-terminy mt-5">
            <div class="row">
              <div class="col-md-4">
                <div class="termin">
                  <div class="termin-doctor">
                    <div class="termin-icon">
                      <img src="../assets/images/icons/doctor.png" class="img-fluid" alt="Doktor">
                    </div>
                    <div class="termin-title">
                      <h4>Internista</h4>
                    </div>
                  </div>
                  <div class="termin-content">
                    <div class="termin-content-daty">
                      <div class="row">
                        <div class="col-md-12 col-lg-6">
                          <base-button class="termin-button">Dzisiaj 09:00</base-button>
                        </div>
                        <div class="col-md-12 col-lg-6">
                          <base-button class="termin-button">Dzisiaj 09:30</base-button>
                        </div>
                        <div class="col-md-12 col-lg-6">
                          <base-button class="termin-button">Jutro 09:00</base-button>
                        </div>
                        <div class="col-md-12 col-lg-6">
                          <base-button class="termin-button">Jutro 09:00</base-button>
                        </div>
                      </div>
                    </div>
                    <div class="termin-content-more-button">
                      <base-button class="termin-button mt-2">Więcej terminów</base-button>
                    </div>
                  </div>
                </div>
              </div>

              <div class="col-md-4">
                <div class="termin">
                  <div class="termin-doctor">
                    <div class="termin-icon">
                      <img src="../assets/images/icons/opthalmologist.png" class="img-fluid" alt="Doktor">
                    </div>
                    <div class="termin-title">
                      <h4>Okulista</h4>
                    </div>
                  </div>
                  <div class="termin-content">
                    <div class="termin-content-daty">
                      <div class="row">
                        <div class="col-md-12 col-lg-6">
                          <base-button class="termin-button">Dzisiaj 09:00</base-button>
                        </div>
                        <div class="col-md-12 col-lg-6">
                          <base-button class="termin-button">Dzisiaj 09:30</base-button>
                        </div>
                        <div class="col-md-12 col-lg-6">
                          <base-button class="termin-button">Jutro 09:00</base-button>
                        </div>
                        <div class="col-md-12 col-lg-6">
                          <base-button class="termin-button">Jutro 09:00</base-button>
                        </div>
                      </div>
                    </div>
                    <div class="termin-content-more-button">
                      <base-button class="termin-button mt-2">Więcej terminów</base-button>
                    </div>
                  </div>
                </div>
              </div>

              <div class="col-md-4">
                <div class="termin">
                  <div class="termin-doctor">
                    <div class="termin-icon">
                      <img src="../assets/images/icons/gastroenterologist.png" class="img-fluid" alt="Doktor">
                    </div>
                    <div class="termin-title">
                      <h4>Gastrolog</h4>
                    </div>
                  </div>
                  <div class="termin-content">
                    <div class="termin-content-daty">
                      <div class="row">
                        <div class="col-md-12 col-lg-6">
                          <base-button class="termin-button">Dzisiaj 09:00</base-button>
                        </div>
                        <div class="col-md-12 col-lg-6">
                          <base-button class="termin-button">Dzisiaj 09:30</base-button>
                        </div>
                        <div class="col-md-12 col-lg-6">
                          <base-button class="termin-button">Jutro 09:00</base-button>
                        </div>
                        <div class="col-md-12 col-lg-6">
                          <base-button class="termin-button">Jutro 09:00</base-button>
                        </div>
                      </div>
                    </div>
                    <div class="termin-content-more-button">
                      <base-button class="termin-button mt-2">Więcej terminów</base-button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import { mapGetters } from 'vuex';
//import axios from 'axios'
//import jwt_decode from "jwt-decode";

export default {
  name: "HomeView",
  computed: {
      ...mapGetters(['user', 'patient'])
  },

  async created(){
      // const token = localStorage.getItem('token');
      // const tokenDecoded = jwt_decode(token);
      // const getPatientInfo = await axios.get(`Patients/${tokenDecoded.nameid}`);
      // await this.$store.dispatch('patient', getPatientInfo.data.data);
  },
};
</script>


<style lang="scss" scoped>
  
  h1 {
      margin: 60px 0 20px;
      font-weight: 700;
      font-weight: 700;
      font-size: 52px;
      line-height: 60px;
      text-align: center;
      letter-spacing: -0.01em;
      color: $button-dark;
  }

  h2 {
    font-weight: 700;
    font-size: 32px;
    line-height: 40px;
    letter-spacing: -0.02em;
    color: $button-dark;
  }

  p {
    font-size: 16px;
    color: $secondary;
  }

  p.home-text {
    font-size: 18px;
  }

  section#home {
    background-color: $primary;
  }

  .termin-button {
    background-color: #E6E9EC!important; 
    border: 1px solid #D1D9E2!important; 
    color: rgb(95, 109, 126)!important;
    font-weight: 600!important;
    width: 100%;
    margin: 5px 0;
  }

  .termin-title h4 {
    font-weight: 600;
    font-size: 22px;
    line-height: 30px;
    text-align: center;
    letter-spacing: -0.01em;
    color: #5F6D7E;
    padding-bottom: 10px;
  }

  .termin-icon img {
    width: 80px;
  }

  .termin {
    background: #F8F9FB;
    border: 1px solid #D1D9E2;
    box-shadow: 0px 1px 2px rgba(16, 24, 40, 0.05);
    border-radius: 5px;
    padding: 25px;
    margin-bottom: 35px;
  }
</style>