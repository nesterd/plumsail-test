<template>
  <div id="app">
    <div>
      <b-navbar toggleable="lg" type="dark" variant="secondary">
        <b-container>
            <b-navbar-brand to="/">Home</b-navbar-brand>

            <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

            <b-collapse id="nav-collapse" is-nav>
              <b-navbar-nav>
                <b-nav-item to="/somethings">Somethings</b-nav-item>
              </b-navbar-nav>
              <b-navbar-nav>
                <b-nav-item to="/about">About</b-nav-item>
              </b-navbar-nav>

              <!-- Right aligned nav items -->
              <b-navbar-nav class="ml-auto">
                <b-nav-item-dropdown right>
                  <!-- Using 'button-content' slot -->
                  <template v-slot:button-content>
                    <em>User</em>
                  </template>
                  <b-dropdown-item href="#">Profile(not done)</b-dropdown-item>
                  <b-dropdown-item href="#">Sign Out(not done)</b-dropdown-item>
                </b-nav-item-dropdown>
              </b-navbar-nav>
            </b-collapse>
        </b-container>
        
      </b-navbar>
    </div>
    <b-container class="main-content">
      <router-view/>
    </b-container>
  </div>
</template>

<style>
.main-content{
  margin-top: 20px;
}

.flter{
  margin-bottom: 10px;
}

#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
