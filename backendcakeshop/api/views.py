from django.shortcuts import render
from django.http import JsonResponse
from cakeshop.models import * 
from .serializer import * 
from rest_framework.decorators import api_view
from rest_framework.response import Response
from rest_framework.viewsets import ModelViewSet, GenericViewSet
from rest_framework.mixins import (
    CreateModelMixin,
    RetrieveModelMixin,
    DestroyModelMixin,
)

# Create your views here.

class CategoryViewSet(ModelViewSet):
    queryset = Category.objects.all()
    serializer_class = CategorySerializer

class ProductViewSet(ModelViewSet):
    queryset = Products.objects.all()
    serializer_class = ProductSerializer

class RoleViewSet(ModelViewSet):
    queryset = Roles.objects.all()
    serializer_class = RoleSerializer

class AccountViewSet(ModelViewSet):
    queryset = Accounts.objects.all()
    serializer_class = AccountSerializer

class PageViewSet(ModelViewSet):
    queryset = Pages.objects.all()
    serializer_class = PageSerializer

class TinDangViewSet(ModelViewSet):
    queryset = TinDang.objects.all()
    serializer_class = TinDangSerializer

class TranSactStatusViewSet(ModelViewSet):
    queryset = TranSactStatus.objects.all()
    serializer_class = TranSactStatusSerializer

# class OrderViewSet(ModelViewSet):
#     queryset = Order.objects.all()
#     serializer_class = OrderSerializer

# class OrderDetailViewSet(ModelViewSet):
#     queryset = OrderDetail.objects.all()
#     serializer_class = OrderDetailSerializer


# giỏ hàng 

class OrderViewSet(
    CreateModelMixin,
    RetrieveModelMixin,
    DestroyModelMixin,
    GenericViewSet,
):
    queryset = Order.objects.all()
    serializer_class = OrderSerializer

class OrderDetailViewSet(ModelViewSet):
    # http_method_names = [
    #     "get",
    #     "post",
    #     "patch",
    #     "delete",
    # ]
    # def get_queryset(self):
    #     return OrderDetail.objects.filter(id=self.kwargs["order_pk"])

    # def get_serializer_class(self):
    #     if self.request.method == "POST":
    #         return AddOrderDetailSerializer
    #     if self.request.method == "PATCH":
    #         return UpdateOrderDetailSerializer
    #     return OrderDetailSerializer
    # def get_serializer_context(self):
    #     return {"id": self.kwargs["order_pk"]}


    queryset = OrderDetail.objects.all()
    serializer_class = OrderDetailSerializer

