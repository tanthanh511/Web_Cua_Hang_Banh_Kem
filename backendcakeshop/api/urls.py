from django.urls import path, include
from . import views
from .views import *
from rest_framework.routers import DefaultRouter
from rest_framework_nested import routers

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
# router.register("orderdetail", OrderDetailViewSet)



order_router = routers.NestedDefaultRouter(router, "order", lookup="orders")
order_router.register("items", views.OrderDetailViewSet, basename="order-items")




urlpatterns = [
    path("", include(router.urls)),
    path("", include(order_router.urls)),
]