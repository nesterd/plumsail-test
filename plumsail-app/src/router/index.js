import Vue from 'vue'
import VueRouter from 'vue-router'
import Somethings from '../views/Somethings'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: () => import('../views/Home.vue')
  },
  {
    path: '/somethings',
    name: 'somethings',
    component: Somethings
  },
  {
    path: '/about',
    name: 'about',
    component: () => import('../views/About.vue')
  }
]

const router = new VueRouter({
  routes
})

export default router
