module Delivery

type Product (id : int, name : string, price : float) =
    let mutable m_price = price
    let mutable m_name = name
    member _.Id = id 
    member _.Price
        with get() = m_price 
        and set(value) = m_price <- value
    member _.Name
            with get() = m_name 
            and set(value) = m_name <- value

[<AbstractClass>]
type Person (id : int, firstName : string, lastName : string, phoneNumber : string) =
    member _.Id = id
    member _.FirstName = firstName
    member _.LastName = lastName
    member _.PhoneNumber = phoneNumber

type Order(id : int, address : string, product : Product) = 
    member _.Id = id
    member _.Address = address
    member _.Product = product

type ICouirer =
    abstract Deliver : order : Order -> unit
    abstract CancelCurrentOrder : unit -> unit
    
type BicycleCoirier(id : int, firstName : string, lastName : string, phoneNumber : string) = 
    inherit Person(id,firstName, lastName, phoneNumber)
    interface ICouirer with
        member _.Deliver(order : Order) =
            printf "Начинаю крутить педали. Заказ №%d по адресу %s с продуктом - %s \n" order.Id order.Address order.Product.Name
        member _.CancelCurrentOrder() =
            printf "Кручу обратно \n"

type CarCoirier(id : int, firstName : string, lastName : string, phoneNumber : string) = 
    inherit Person(id,firstName, lastName, phoneNumber)
    interface ICouirer with
        member _.Deliver(order : Order) =
            printf "Выезжаю к заказчику. Заказ №%d по адресу %s с продуктом - %s \n" order.Id order.Address order.Product.Name
        member _.CancelCurrentOrder() =
            printf "Разворачиваюсь обратно \n"