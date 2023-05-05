from django.urls import path, include
from .views import *
from rest_framework.routers import DefaultRouter

# localhost mặc định là:
router = DefaultRouter()

# localhost của mỗi bảng: http://127.0.0.1:8000/ + router
router.register("category", CategoryViewSet)
router.register("product", ProductViewSet)
router.register("role", RoleViewSet)
router.register("account", AccountViewSet)
router.register("page", PageViewSet)
router.register("tindang", TinDangViewSet)
router.register("transactstatus", TranSactStatusViewSet)
router.register("order", OrderViewSet)
router.register("orderdetail", OrderDetailViewSet)
urlpatterns = [path("", include(router.urls))]