---
layout: post
title: "Domain-Driven Design Glossary"
subtitle: "Summary of the main terms in Domain-Driven Design"
author: "Devon Burriss"
category: Project Management
tags: [DDD]
comments: true
permalink: ddd-glossary
excerpt_separator: <!--more-->
header-img: "img/backgrounds/vents-bg.jpg"
published: false
---

# Domain Driven Design

DDD cannot be summarized in a few paragraphs. In fact it would take a few books to cover it thoroughly. 
Even then like anything worthwhile it requires much practice and many mistakes to start to become proficient at it.
This is how it is with most skills that add a lot of value.

## What is DDD not?

DDD is not:  

* Calling your area of work a Domain
* Modelling the state of objects required into a bunch of [anemic models](http://www.martinfowler.com/bliki/AnemicDomainModel.html)
* Services containing logic that act on the anemic models

## What is DDD?

DDD is about modelling. It encompasses techniques, pattens, language and architecture. In short it encompasses more than just modelling. 
It is about taking requirements and really mapping the business processes to the model using the same language the business uses in your code.
It also gives us a common technical language to use for the different categories of classes we create while modelling our problem space.


## Glossary of terms

### Ubiquitous language

The term *Ubiquitous language* is thrown out occationally in DDD discussions but ironically itself is often not discussed. It is also the part often left out from the development side which means the heart of DDD is not being followed and instead some of its' technical approaches used (often incorrectly).  
It is the practice of **using the terms used throughout the business within the codebase**. Language often evolves and the codebase should evolve with the language. The essence really of DDD is that your code models the processes within the business and if you are not starting with the same language then how descriptive can it really be. If a product owner is looking at the application code he should recognise the classes, methods, and variables as models, workflows, and actions that actually occur.

It is not a one-way street however. Often the business has over-loaded terms, or a multiplicity of terms used for the same thing. Work with the them to define a glossary of terms that is used everywhere (ubiquitously). 

### Bounded context

The *Bounded context* is the context in which the *Ubiquitous language* and the corresponding models are valid. As developer it is a common trap to fall into to try reuse code and concepts across contexts. This is a recipe for disaster since the terms and verbs used to describe a model in one context will likely be similar but not the same. This results in blurring of the model to cater for both. This adds confusion as well as inviting changes with unintended consequences. This is especially true when a model is shared across more than one team.

### Entities

Entities are the classes that model the domain concepts and have identity. This usually means there is a unique primary key associated with the entity. Remember that modelling in DDD takes us back to the OOP we learned in the text books... behavior and data together. This is in antithesis to the usual [anemic models](https://martinfowler.com/bliki/AnemicDomainModel.html) found in most software.

### Value objects

Value objects are much like entities except they do not have identity. Money is the quintessential example of a model that shows intent, contains rules, but does not have identity. The important part here is using types to convey meaning as well as place logic along with the data in a very obvious way.

### Aggregate

An Aggregate is a hierachy of objects (Entities and Value objects) that make up a consistency boundary.  
Why would we want to set a boundary rather than just reference any object needed?  

Minimising associations helps to prevent a reference web. This can be problematic when fetching and reconstituting a hierachy of objects into memory. Lazy loading can quickly get out of hand, alternatively null references about and conntinually need to be checked.

Let us turn the question around. What if the relationships of our object model clearly showed us the effects of change? For example, the aggregate was the scope of the transaction...

#### Aggregate root

The Aggregate Root is an Entity that all other Entities and Value Objects in the hierachy hang off. For example if you have an Order with Order Lines and a Supplier, the `OrderRepository` will return an Order with all `OrderLines` and `OrderSupplier` populated. If would not be possible to fetch an `OrderLine` separately, nor a `OrderSupplier`. If needed though you would provide methods on your `OrderRepository` to fetch an order by Order Line Id or by Supplier Reference for example.

#### Points to keep in mind

- Technical difficulties implementing an aggregate (like transaction issues persisting it) are usually indicative of a poorly chosen model. Put more effort refining the model rather than trying to fix a modelling problem with a technical implementation.
- Access to objects from outside the aggregate must occur through the Aggregate Root.
- Aggregates are always constructed in a consistent state.
- The logic is usually within the aggregate to disallow consistent state or at least check its consistency.
- It is better to encapsulate changes to state through method calls rather than directly mutating properties. This shows intent as well as adds an extra layer of indirection allowing implemntation changes without changing the API.

### Factories

Since an aggregate should always be in a consistent state it is important that they are constructed in a in a consistent state to the user. Factories provide a way to **ensure that new instances of an aggregate always start in a consistent state**.

### Repositories

Repositories protect us from taking a data-centric view of our code. They allow us to **persist and retrieve aggregates** without dealing directly with the underlying persistence. It is however important for developers to at least be aware of the underlying implementations so as not to abuse the repository from a performance or scoping way.

### Services

### Application layer

### Anti-corruption layer

### Infrastructure

### Clients

### Further reading