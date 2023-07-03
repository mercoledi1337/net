<template>
  <TheHeader/>
  <router-view></router-view>
  <TheFooter/>
</template>

<script>
import TheHeader from "@/components/TheHeader.vue";
import TheFooter from "@/components/TheFooter.vue";


export default {
  components: {
    TheHeader, 
    TheFooter,
  },
};
</script>

<style lang="scss">
@import url('https://fonts.googleapis.com/css2?family=Inter:wght@100;200;300;400;500;600;700;800;900&display=swap');

#app {
  font-family: 'Inter', sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  display: flex;
  flex-direction: column;
  min-height: 100vh;

  section {
    min-height: calc(100vh - 289px);
  }

}

nav {
  padding: 30px;

  a {
    font-weight: bold;
    color: #2c3e50;
  }
}


</style>
